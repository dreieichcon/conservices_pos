using Innkeep.Db.Models;

namespace Innkeep.Db.Server.Models;

public class PretixConfig : AbstractDbItem
{
	public string? PretixAccessToken { get; set; }
	
	public string? SelectedOrganizerSlug { get; set; }
	
	public string? SelectedOrganizerName { get; set; }
	
	public string? SelectedEventSlug { get; set; }
	
	public string? SelectedEventName { get; set; }
}