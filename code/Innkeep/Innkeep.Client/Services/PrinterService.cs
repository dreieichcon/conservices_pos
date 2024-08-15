using DemoPOS.Document;
using DemoPOS.Helpers;
using Innkeep.Api.Models.Internal;
using Innkeep.Client.Extensions;
using Innkeep.Services.Client.Interfaces.Hardware;

namespace Innkeep.Client.Services;

public class PrinterService : IPrinterService
{
	public void TestPrint(string printerName)
	{
		var manager = new DocumentManager(printerName)
					.AddEmptyLines(3)
					.AddTitle("Test Page")
					.AddEmptyLines(3)
					.AddLine(new string('#', 42))
					.Cut();
		
		manager.Print();
	}

	public void PrintReceipt(string printerName, TransactionReceipt receipt)
	{
		var manager = new DocumentManager(printerName)
					.AddTitle(receipt.Title)
					.AddSeparatedLines(receipt.Header)
					.AddDashedLine()
					.AddReceiptLines(receipt)
					.AddReceiptSum(receipt)
					.AddReceiptTaxIfExists(receipt)
					.AddQrCode(receipt.QrCode)
					.AddEmptyLine()
					.Cut();
		
		manager.Print();
	}

	public List<string> PrinterNames { get; set; } = RegistryHelper.GetInstalledPrinterNames().ToList();

}