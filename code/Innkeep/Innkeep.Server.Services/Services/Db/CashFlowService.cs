using Innkeep.Core.DomainModels.Dashboard;
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

	public List<RegisterCashInfo> GetCurrentCashState()
	{
		if (_applicationSettingsService.ActiveSetting.SelectedEvent == null) return new List<RegisterCashInfo>();

		var cashFlows = _cashFlowRepository
						.GetAllCustom(x => x.Event.Slug == _applicationSettingsService.ActiveSetting.SelectedEvent.Slug)
						.GroupBy(x => x.Register);

		return GetCashInfo(cashFlows);
	}

	private List<RegisterCashInfo> GetCashInfo(IEnumerable<IGrouping<Register, CashFlow>> cashFlows)
	{
		return cashFlows.Select(
							grouping => new RegisterCashInfo()
							{
								Register = grouping.Key,
								Event = grouping.First().Event,
								RegisterId = grouping.Key.DeviceId,
								CashState = grouping.Sum(x => x.TotalMoney),
							}
						)
						.ToList();
	}
}