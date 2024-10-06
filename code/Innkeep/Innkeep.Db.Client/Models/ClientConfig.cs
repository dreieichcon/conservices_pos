using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demolite.Db.Models;

namespace Innkeep.Db.Client.Models;

public class ClientConfig : AbstractDbItem
{
	[MaxLength(63)]
	public string ServerAddress { get; set; } = "https://127.0.0.1:1337";

	[MaxLength(255)]
	public string ClientName { get; set; } = "Innkeep Client";

	[MaxLength(255)]
	public string PrinterName { get; set; } = string.Empty;

	[NotMapped]
	public string HardwareIdentifier { get; set; } = "";
}