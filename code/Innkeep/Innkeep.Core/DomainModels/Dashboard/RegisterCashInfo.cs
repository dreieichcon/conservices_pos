using Innkeep.Server.Data.Models;

namespace Innkeep.Core.DomainModels.Dashboard;

public class RegisterCashInfo
{
	public required string RegisterId { get; set; }
	
	public Register Register { get; set; }
	
	public Event Event { get; set; }
    
	public decimal CashState { get; set; }
}