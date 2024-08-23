using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Services.Client.Interfaces.Hardware;

public interface IPrinterService
{
	public List<string> PrinterNames { get; set; }

	public void TestPrint(string printerName);

	public void PrintReceipt(string printerName, TransactionReceipt receipt);

	public void PrintReceipt(string printerName, TransferReceipt receipt);

	void OpenDrawer(string printerName);
}