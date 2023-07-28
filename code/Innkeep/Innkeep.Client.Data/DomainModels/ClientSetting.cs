namespace Innkeep.Client.Data.DomainModels;

public class ClientSetting
{
	public Uri ServerUri { get; set; } = new($"https://127.0.0.1:1338");

	public string PrinterComPort { get; set; } = "";

	public string CardReaderComPort { get; set; } = "";
}