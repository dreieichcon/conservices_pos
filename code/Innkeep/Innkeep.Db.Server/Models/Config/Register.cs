using System.ComponentModel.DataAnnotations;
using Demolite.Db.Models;

namespace Innkeep.Db.Server.Models.Config;

public class Register : AbstractDbItem
{
	[MaxLength(255)]
	public string RegisterIdentifier { get; set; } = "";

	[MaxLength(255)]
	public string RegisterDescription { get; set; } = "";

	[MaxLength(255)]
	public string LastHostname { get; set; } = "";
}