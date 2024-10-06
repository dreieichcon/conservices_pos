using System.ComponentModel.DataAnnotations;
using Demolite.Db.Models;

namespace Innkeep.Db.Server.Models.Config;

public class FiskalyConfig : AbstractDbItem
{
	[MaxLength(255)]
	public string ApiKey { get; set; } = string.Empty;

	[MaxLength(255)]
	public string ApiSecret { get; set; } = string.Empty;

	[MaxLength(63)]
	public string TseId { get; set; } = string.Empty;

	[MaxLength(63)]
	public string ClientId { get; set; } = string.Empty;
}