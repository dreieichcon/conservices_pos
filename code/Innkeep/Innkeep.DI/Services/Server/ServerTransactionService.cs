using Innkeep.Core.DomainModels.Print;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Shared.Objects.Transaction;
using Transaction = Innkeep.Shared.Objects.Transaction.Transaction;

namespace Innkeep.DI.Services.Server;

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

	public async Task<Receipt> CreateTransaction(Transaction transaction, Register register)
	{
		var pretixResult = await _pretixService.CreateOrder(transaction);

		var tseResult = await _tseService.CreateEntry(transaction);
		tseResult.StartTime = transaction.TransactionStart;

		var guid = Guid.NewGuid();

		var dbTransaction = new Innkeep.Server.Data.Models.Transaction()
		{
			TseToken = tseResult.TseTransactionNumber,
			PretixOrderNumber = pretixResult.Code,
			Organizer = _applicationSettingsService.ActiveSetting.SelectedOrganizer!,
			Event = _applicationSettingsService.ActiveSetting.SelectedEvent!,
			Device = register,
			TransactionId = guid,
			TimeStamp = DateTime.Now,
			Items = transaction.SerializeTransaction()
		};

		_transactionRepository.Create(dbTransaction);
		_cashFlowService.CreateCashFlow(register, transaction);

		return ReceiptGenerator.Generate(
			new TransactionServiceResult()
			{
				EventName = _pretixService.SelectedEvent!.Name.German,
				RegisterId = register.DeviceId,
				OrganizerInfo = _applicationSettingsService.ActiveSetting.OrganizerInfo,
				Transaction = transaction,
				OrderResponse = pretixResult,
				TseResult = tseResult,
				Guid = guid
			}
		);
	}
}