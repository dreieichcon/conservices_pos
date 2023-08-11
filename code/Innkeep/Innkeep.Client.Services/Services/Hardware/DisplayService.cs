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
		var port = GetSerialPort();
		port?.Write("\r\n");
		port?.Write(text);
		port?.Close();
	}

	public void ShowText(string text)
	{
		var port = GetSerialPort();
		port?.Write("\r\n");
		port?.Write(text);
		port?.Close();
	}

	public void ClearText()
	{
		var port = GetSerialPort();
		port?.Write("\r\n");
		port?.Write(new string(' ', 20));
		port?.Close();
	}

	private SerialPort? GetSerialPort()
	{
		try
		{
			var port = new SerialPort(
				_clientSettingsService.Setting.DisplayComPort,
				_clientSettingsService.Setting.DisplayBaudRate
			);

			try
			{
				port.Open();
				port.Write(string.Empty);
				return port;
			}
			catch
			{
				port.Close();

				try
				{
					port.Open();
					port.Write(string.Empty);

					return port;
				}
				catch
				{
					return null;
				}
			}
		}
		catch
		{
			return null;
		}

		return null;
	}
}