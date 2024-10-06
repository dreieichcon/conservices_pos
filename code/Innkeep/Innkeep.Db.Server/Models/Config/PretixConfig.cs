using System.ComponentModel.DataAnnotations;
using Demolite.Db.Models;

namespace Innkeep.Db.Server.Models.Config;

public class PretixConfig : AbstractDbItem
{
	[MaxLength(256)]
	public string? PretixAccessToken { get; set; }

	[MaxLength(32)]
	public string? SelectedOrganizerSlug { get; set; }

	[MaxLength(256)]
	public string? SelectedOrganizerName { get; set; }

	[MaxLength(32)]
	public string? SelectedEventSlug { get; set; }

	[MaxLength(256)]
	public string? SelectedEventName { get; set; }

	public int? SelectedCheckinListId { get; set; }

	[MaxLength(256)]
	public string? SelectedCheckinListName { get; set; }
}