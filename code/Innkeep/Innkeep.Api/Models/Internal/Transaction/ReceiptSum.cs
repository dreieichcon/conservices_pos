using System.Globalization;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Internal;

public class ReceiptSum
{
	public decimal TotalAmount { get; set; }
	
	public decimal AmountGiven { get; set; }
	
	public decimal AmountReturned { get; set; }
	
	[JsonIgnore]
	public string TotalAmountString => TotalAmount.ToString("F2", CultureInfo.InvariantCulture);
	
	[JsonIgnore]
	public string AmountGivenString => AmountGiven.ToString("F2", CultureInfo.InvariantCulture);

	[JsonIgnore]
	public string AmountReturnedString => (-AmountReturned).ToString("F2", CultureInfo.InvariantCulture);

}