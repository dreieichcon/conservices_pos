using Innkeep.Db.Models;

namespace Innkeep.Server.Db.Models;

public class FiskalyConfig : AbstractDbItem
{
	public string? ApiKey { get; set; }
	
	public string? ApiSecret { get; set; }
	
	public string? Token { get; set; }
	
	public DateTime? TokenValidUntil { get; set; }

	public string TseId { get; set; } = string.Empty;

	public string ClientId { get; set; } = string.Empty;
	
	
}