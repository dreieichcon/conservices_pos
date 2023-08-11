using System.IO.Ports;
using Innkeep.Client.Services.Interfaces.File;
using Innkeep.Client.Services.Interfaces.Hardware;

namespace Innkeep.Client.Services.Services.Hardware;

public class DisplayService : IDisplayService
{
	private readonly IClientSettingsService _clientSettingsService;

	public DisplayService(IClientSettingsService clientSettingsService)
	{
		_clientSettingsService = clientSettingsService;
	}
	

	public void TestDisplay()
	{
		var text = "Display Test INNKEEP";
		GetSerialPort().Write(text);
	}

	public void ShowText(string text)
	{
		GetSerialPort().Write(text);
	}

	public void ClearText()
	{
		GetSerialPort().Write(string.Empty);
	}

	private SerialPort GetSerialPort()
	{
		return new SerialPort(
			_clientSettingsService.Setting.DisplayComPort,
			_clientSettingsService.Setting.DisplayBaudRate
		);
	}
}