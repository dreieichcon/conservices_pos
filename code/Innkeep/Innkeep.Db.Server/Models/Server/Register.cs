using System.ComponentModel.DataAnnotations;
using Innkeep.Db.Models;

namespace Innkeep.Db.Server.Models.Server;

public class Register : AbstractDbItem
{
	[MaxLength(255)]
	public string RegisterIdentifier { get; set; } = "";
	
	[MaxLength(255)]
	public string RegisterDescription { get; set; } = "";
	
	[MaxLength(255)]
	public string LastHostname { get; set; } = "";
}