namespace Innkeep.Client.Services.Models;

public class ClientSetting
{
	public Uri ServerUri { get; set; } = new($"https://127.0.0.1:1338");

	public string PrinterComPort { get; set; } = "";

	public string CardReaderComPort { get; set; } = "";

	public string DisplayComPort { get; set; } = "";

	public int DisplayBaudRate { get; set; } = 9600;
}