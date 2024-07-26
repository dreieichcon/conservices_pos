namespace Innkeep.Services.Client.Interfaces.Hardware;

public interface IPrinterService
{
	public List<string> PrinterNames { get; set; }

	public void TestPrint(string printerName);
}