using Innkeep.Client.Interfaces.Services;
using Innkeep.Printer.Document;

namespace Innkeep.DI.Services;

public class PrintService : IPrintService
{
	private readonly IClientSettingsService _clientSettingsService;

	public PrintService(IClientSettingsService clientSettingsService)
	{
		_clientSettingsService = clientSettingsService;
	}
	
	public void TestPage()
	{
		if (string.IsNullOrEmpty(_clientSettingsService.Setting.PrinterComPort)) return;

		var manager = new DocumentManager(_clientSettingsService.Setting.PrinterComPort);
		manager.AddTitle("Test Page").Cut().Print();

	}
}