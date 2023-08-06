using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Services.Interfaces.Api;
using Innkeep.Server.Services.Interfaces.Db;
using Innkeep.Server.Services.Models;
using Innkeep.Server.Services.Util;

namespace Innkeep.Server.Services.Services.Api;

public class ServerTransactionService : IServerTransactionService
{
	private readonly IPretixService _pretixService;
	private readonly ITseService _tseService;
	private readonly ITransactionRepository _transactionRepository;
	private readonly IApplicationSettingsService _applicationSettingsService;
	private readonly ICashFlowService _cashFlowService;

	public ServerTransactionService
	(IPretixService pretixService,
	ITseService tseService,
	ITransactionRepository transactionRepository,
	IApplicationSettingsService applicationSettingsService,
	ICashFlowService cashFlowService)
	{
		_pretixService = pretixService;
		_tseService = tseService;
		_transactionRepository = transactionRepository;
		_applicationSettingsService = applicationSettingsService;
		_cashFlowService = cashFlowService;
	}

	public async Task<Receipt> CreateTransaction(PretixTransaction pretixTransaction, Register register)
	{
		var pretixResult = await _pretixService.CreateOrder(pretixTransaction);
		if (pretixResult is null) return new Receipt();

		var tseResult = await _tseService.CreateEntry(pretixTransaction);

		var guid = Guid.NewGuid();

		var dbTransaction = new Transaction()
		{
			TseToken = tseResult.TseTransactionNumber,
			PretixOrderNumber = pretixResult.Code,
			Organizer = _applicationSettingsService.ActiveSetting.SelectedOrganizer!,
			Event = _applicationSettingsService.ActiveSetting.SelectedEvent!,
			Device = register,
			TransactionId = guid,
			TimeStamp = DateTime.Now,
			Items = pretixTransaction.SerializeTransaction()
		};

		_transactionRepository.Create(dbTransaction);
		_cashFlowService.CreateCashFlow(register, pretixTransaction);

		return ReceiptGenerator.Generate(
			new TransactionServiceResult()
			{
				EventName = _pretixService.SelectedEvent!.Name.German,
				RegisterId = register.DeviceId,
				OrganizerInfo = _applicationSettingsService.ActiveSetting.OrganizerInfo,
				PretixTransaction = pretixTransaction,
				OrderResponse = pretixResult,
				TseResult = tseResult,
				Guid = guid
			}
		);
	}
}