using DemoPOS.Document;
using DemoPOS.Helpers;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Interfaces;

namespace Innkeep.Client.Services;

public class PrinterService : IPrinterService
{
	public PrinterService()
	{
		PrinterNames = RegistryHelper.GetInstalledPrinterNames().ToList();
	}

	public void TestPrint(string printerName)
	{
		var manager = new DocumentManager(printerName).AddEmptyLines(3).AddTitle("Test Page").AddEmptyLines(3).Cut();
		manager.Print();
	}

	public List<string> PrinterNames { get; set; }
}