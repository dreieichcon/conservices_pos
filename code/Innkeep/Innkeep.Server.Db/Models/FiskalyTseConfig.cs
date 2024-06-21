using Innkeep.Db.Models;

namespace Innkeep.Server.Db.Models;

public class FiskalyTseConfig : AbstractDbItem
{
	public required string TseId { get; set; }
	
	public string? TsePuk { get; set; }
	
	public string? TseAdminPassword { get; set; }
}