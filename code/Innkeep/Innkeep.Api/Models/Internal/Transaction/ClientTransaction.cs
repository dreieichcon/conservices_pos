using System.Text.Json.Serialization;
using Innkeep.Api.Enum.Fiskaly.Transaction;

namespace Innkeep.Api.Models.Internal;

public class ClientTransaction
{
	public IEnumerable<DtoSalesItem> SalesItems { get; set; } = [];
	
	public PaymentType PaymentType { get; set; }
	
	public decimal AmountGiven { get; set; }
	
	public string Currency { get; set; }
	
	[JsonIgnore]
	public decimal AmountNeeded => SalesItems.Sum(x => x.TotalPrice);

	[JsonIgnore]
	public decimal AmountBack => AmountNeeded - AmountGiven;
}