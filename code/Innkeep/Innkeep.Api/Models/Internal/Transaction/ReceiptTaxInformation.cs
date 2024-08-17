using System.Globalization;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Internal;

public class ReceiptTaxInformation
{
	public string Name { get; set; } = "";

	public decimal Net { get; set; }

	public decimal TaxAmount { get; set; }

	public decimal Gross { get; set; }

	[JsonIgnore]
	public string NameString => Name.Replace(" ", "").PadRight(5);

	[JsonIgnore]
	public string NetString => Net.ToString("F2", CultureInfo.InvariantCulture).PadRight(6);
	
	[JsonIgnore]
	public string TaxAmountString => TaxAmount.ToString("F2", CultureInfo.InvariantCulture).PadRight(6);
	
	[JsonIgnore]
	public string GrossString => Gross.ToString("F2", CultureInfo.InvariantCulture).PadRight(6);
}