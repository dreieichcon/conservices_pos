using Innkeep.Server.Data.Models;

namespace Innkeep.Server.WebUi.Models;

public class RegisterCashInfo
{
	public required string RegisterId { get; set; }
	
	public Register Register { get; set; }
	
	public Event Event { get; set; }
    
	public decimal CashState { get; set; }

	public static List<RegisterCashInfo> GetCashInfo(IEnumerable<IGrouping<Register, CashFlow>> cashFlows)
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