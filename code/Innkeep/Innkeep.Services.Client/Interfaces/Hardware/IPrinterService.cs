using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;

namespace Innkeep.Services.Client.Interfaces.Hardware;

public interface IPrinterService
{
	public List<string> PrinterNames { get; set; }

	public void TestPrint(string printerName);

	public void PrintReceipt(string printerName, TransactionReceipt receipt);
}