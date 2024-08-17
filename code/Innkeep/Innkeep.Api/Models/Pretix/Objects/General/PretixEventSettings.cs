using System.Text.Json.Serialization;

namespace Innkeep.Api.Models.Pretix.Objects.General;

public class PretixEventSettings
{
	[JsonPropertyName("invoice_address_from_name")]
	public string InvoiceCompanyName { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from")]
	public string InvoiceStreetAddress { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from_zipcode")]
	public string InvoiceZipCode { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from_city")]
	public string InvoiceCity { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from_country")]
	public string InvoiceCountry { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from_tax_id")]
	public string InvoiceTaxId { get; set; } = "";
	
	[JsonPropertyName("invoice_address_from_vat_id")]
	public string InvoiceVatId { get; set; } = "";
}