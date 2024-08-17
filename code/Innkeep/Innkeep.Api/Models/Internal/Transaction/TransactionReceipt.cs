namespace Innkeep.Api.Models.Internal.Transaction;

public class TransactionReceipt
{
	public string Title { get; set; } = "";
	
	public string Header { get; set; } = "";
	
	public string Currency { get; set; } = "";

	public string TransactionId { get; set; } = "";
	
	public long TransactionCounter { get; set; }
	
	public List<ReceiptLine> Lines { get; set; } = [];
	
	public ReceiptSum Sum { get; set; }
	
	public List<ReceiptTaxInformation> TaxInformation { get; set; }

	public string QrCode { get; set; } = "";
}