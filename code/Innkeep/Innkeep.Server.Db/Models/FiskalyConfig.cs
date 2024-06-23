using Innkeep.Db.Models;

namespace Innkeep.Server.Db.Models;

public class FiskalyConfig : AbstractDbItem
{
	public string ApiKey { get; set; } = string.Empty;
	
	public string ApiSecret { get; set; } = string.Empty;

	public string TseId { get; set; } = string.Empty;

	public string ClientId { get; set; } = string.Empty;
}