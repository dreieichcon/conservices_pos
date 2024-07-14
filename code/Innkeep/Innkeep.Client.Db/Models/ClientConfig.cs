using Innkeep.Db.Models;

namespace Innkeep.Client.Db.Models;

public class ClientConfig : AbstractDbItem
{
	public string ServerAddress { get; set; } = "https://127.0.0.1/";
}