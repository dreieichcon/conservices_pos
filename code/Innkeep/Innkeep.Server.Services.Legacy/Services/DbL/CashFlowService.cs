
using Innkeep.Server.Data.Models;
using Innkeep.Models.Transaction;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Services.Legacy.Interfaces.Api;
using Innkeep.Server.Services.Legacy.Interfaces.Db;

namespace Innkeep.Server.Services.Legacy.Services.Db;

public class CashFlowService : ICashFlowService
{
	private readonly ICashFlowRepository _cashFlowRepository;
	private readonly IApplicationSettingsService _applicationSettingsService;
	private readonly IFiskalyService _fiskalyService;

	public CashFlowService
		(ICashFlowRepository cashFlowRepository, IApplicationSettingsService applicationSettingsService, IFiskalyService fiskalyService)
	{
		_cashFlowRepository = cashFlowRepository;
		_applicationSettingsService = applicationSettingsService;
		_fiskalyService = fiskalyService;
	}
	
	public void CreateTransactionCashFlow(Register register, PretixTransaction pretixTransaction)
	{
		_cashFlowRepository.Create(
			new CashFlow()
			{
				TimeStamp = DateTime.Now,
				Event = _applicationSettingsService.ActiveSetting.SelectedEvent!,
				Register = register,
				MoneyAdded = pretixTransaction.AmountGiven,
				MoneyRemoved = Math.Abs(pretixTransaction.Return),
			}
		);
	}

	public async Task CreateServerCashFlow(CashFlow cashFlow)
	{
		cashFlow.Event = _applicationSettingsService.ActiveSetting.SelectedEvent!;
		await _fiskalyService.CreateCashFlow(cashFlow);
		_cashFlowRepository.Create(cashFlow);
	}

	public IEnumerable<IGrouping<Register, CashFlow>> GetCashFlows()
	{
		var cashFlows = _cashFlowRepository
						.GetAllCustom(x => x.Event.Slug == _applicationSettingsService.ActiveSetting.SelectedEvent.Slug)
						.GroupBy(x => x.Register);

		return cashFlows;
	}
}