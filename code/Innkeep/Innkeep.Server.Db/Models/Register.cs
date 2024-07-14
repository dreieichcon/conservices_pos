using Innkeep.Db.Models;

namespace Innkeep.Server.Db.Models;

public class Register : AbstractDbItem
{
	public string RegisterIdentifier { get; set; }
	
	public string RegisterDescription { get; set; }
}