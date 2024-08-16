using Innkeep.Db.Models;

namespace Innkeep.Db.Server.Models;

public class FiskalyTseConfig : AbstractDbItem
{
	public required string TseId { get; set; }
	
	public string? TsePuk { get; set; }
	
	public string? TseAdminPassword { get; set; }
}