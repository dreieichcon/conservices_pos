using Innkeep.Api.Fiskaly.Data;
using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Interfaces;
using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Interfaces.Api;
using Innkeep.Server.Services.Models;
using Innkeep.Server.Services.Services.Db;

namespace Innkeep.Server.Services.Services.Api;

public class FiskalyService : IFiskalyService
{
	private readonly IFiskalyApiSettingsService _apiSettingsService;
	private readonly IFiskalyAuthenticationRepository _authenticationRepository;
	private readonly IFiskalyTransactionRepository _transactionRepository;

	public FiskalyService(IFiskalyApiSettingsService apiSettingsService, 
						IFiskalyAuthenticationRepository authenticationRepository, 
						IFiskalyTransactionRepository transactionRepository)
	{
		_apiSettingsService = apiSettingsService;
		_authenticationRepository = authenticationRepository;
		_transactionRepository = transactionRepository;
	}
	
	public async Task<TseResult?> CreateTransaction(PretixTransaction transaction)
	{
		if (_apiSettingsService.ApiSettings.TokenValidUntil is null ||
			_apiSettingsService.ApiSettings.TokenValidUntil! < DateTime.Now)
		{
			var newToken = await _authenticationRepository.AuthenticateApi();

			if (newToken == null) return null;

			_apiSettingsService.ApiSettings.Token = newToken.AccessToken;
			_apiSettingsService.ApiSettings.TokenValidUntil = newToken.AccessTokenExpiresAtDateTime;

			_apiSettingsService.Save();
		}

		var start = await _transactionRepository.StartTransaction(transaction);

		if (start is null) return null;

		var updateItem = TransactionUpdateSerializer.Create(_apiSettingsService.ApiSettings.ClientId, transaction);

		var update = await _transactionRepository.UpdateTransaction(updateItem, transaction.TransactionId.ToString());

		updateItem.State = TransactionState.FINISHED;

		var finish = await _transactionRepository.EndTransaction(updateItem, transaction.TransactionId.ToString());

		if (finish is null) return null;
		
		return new TseResult()
		{
			FirstOrder = transaction.TransactionStart,
			StartTime = finish.StartTime,
			EndTime = finish.EndTime,
			TseTransactionNumber = finish.Number.ToString(),
			TseSerialNumber = finish.TssSerialNumber,
			HashAlgorithm = finish.Signature.algorithm,
			Signature = finish.Signature.value,
			TseTimestampFormat = finish.Log.TimestampFormat,
			QrCode = finish.QrCodeData,
		};
	}
}