namespace Innkeep.Api.Models.Internal.Transfer;

public class ClientTransfer
{
	public decimal Amount { get; set; }

	public int Factor => IsRetrieve ? -1 : 1;
	
	public bool IsRetrieve { get; set; }
}