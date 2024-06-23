using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;

namespace Innkeep.Db.Models;

public class AbstractDbItem : IDbItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key, Column(Order = 0)]
	public string Id { get; set; } = Guid.NewGuid().ToString();

	[NotMapped]
	public Operation OperationType { get; set; }
}