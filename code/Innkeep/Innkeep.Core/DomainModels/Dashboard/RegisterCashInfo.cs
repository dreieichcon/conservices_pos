namespace Innkeep.Core.DomainModels.Dashboard;

public class RegisterCashInfo
{
	public required string RegisterId { get; set; }
	
	public decimal CashState { get; set; }
}