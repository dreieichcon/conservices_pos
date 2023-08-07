
using Innkeep.Server.Data.Models;
using Innkeep.Models.Transaction;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Services.Interfaces.Db;

namespace Innkeep.Server.Services.Services.Db;

public class CashFlowService : ICashFlowService
{
	private readonly ICashFlowRepository _cashFlowRepository;
	private readonly IApplicationSettingsService _applicationSettingsService;

	public CashFlowService
		(ICashFlowRepository cashFlowRepository, IApplicationSettingsService applicationSettingsService)
	{
		_cashFlowRepository = cashFlowRepository;
		_applicationSettingsService = applicationSettingsService;
	}
	
	public void CreateCashFlow(Register register, PretixTransaction pretixTransaction)
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

	public void CreateCashFlow(CashFlow cashFlow)
	{
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