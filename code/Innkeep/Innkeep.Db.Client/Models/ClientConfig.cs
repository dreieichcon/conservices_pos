using Innkeep.Db.Models;

namespace Innkeep.Db.Client.Models;

public class ClientConfig : AbstractDbItem
{
	public string ServerAddress { get; set; } = "https://127.0.0.1:1337";

	public string ClientName { get; set; } = "Innkeep Client";

	public string PrinterName { get; set; } = string.Empty;
}