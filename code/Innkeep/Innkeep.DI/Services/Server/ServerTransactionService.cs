using Innkeep.Core.DomainModels.Print;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.DI.Services.Server;

public class ServerTransactionService : IServerTransactionService
{
	private readonly IPretixService _pretixService;
	private readonly ITseService _tseService;
	private readonly ITransactionRepository _transactionRepository;
	private readonly IApplicationSettingsService _applicationSettingsService;

	public ServerTransactionService
	(IPretixService pretixService,
	ITseService tseService,
	ITransactionRepository transactionRepository,
	IApplicationSettingsService applicationSettingsService)
	{
		_pretixService = pretixService;
		_tseService = tseService;
		_transactionRepository = transactionRepository;
		_applicationSettingsService = applicationSettingsService;
	}

	public async Task<Receipt> CreateTransaction(Transaction transaction)
	{
		var pretixResult = await _pretixService.CreateOrder(transaction);

		var tseResult = await _tseService.CreateEntry(transaction);
		
		// TODO - Write To Db

		return ReceiptGenerator.Generate(
			transaction,
			pretixResult,
			tseResult,
			_applicationSettingsService.ActiveSetting.OrganizerInfo,
			_pretixService.SelectedEvent!.Name.German
		);
	}
}