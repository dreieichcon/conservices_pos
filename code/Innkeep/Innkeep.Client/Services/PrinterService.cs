using DemoPOS.Document;
using DemoPOS.Helpers;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;
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
					.AddLine(receipt.BookingTime.ToString("dd.MM.yyyy HH:mm:ss"))
					.AddDashedLine()
					.AddReceiptLines(receipt)
					.AddReceiptSum(receipt)
					.AddReceiptTaxIfExists(receipt)
					.AddDashedLine()
					.AddTransactionInfo(receipt)
					.AddDashedLine()
					.AddQrCode(receipt.QrCode)
					.Cut()
					.AddVouchers(receipt);
		
		manager.Print();
	}

	public void PrintReceipt(string printerName, TransferReceipt receipt)
	{
		var manager = new DocumentManager(printerName)
					.AddTitle("Transferbeleg")
					.AddLine(receipt.BookingTime.ToString("dd.MM.yyyy HH:mm:ss"))
					.AddDashedLine()
					.AddTransferLine(receipt)
					.AddEmptyLines(3)
					.AddDashedLine()
					.AddLine("Unterschrift")
					.AddEmptyLine()
					.AddQrCode(receipt.QrCode)
					.Cut();
		
		manager.Print();
	}

	public void OpenDrawer(string printerName)
	{
		var manager = new DocumentManager(printerName);
		manager.Drawer();
	}

	public List<string> PrinterNames { get; set; } = RegistryHelper.GetInstalledPrinterNames().ToList();

}