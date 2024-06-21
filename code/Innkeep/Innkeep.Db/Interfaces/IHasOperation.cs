using System.ComponentModel.DataAnnotations.Schema;
using Innkeep.Db.Enum;

namespace Innkeep.Db.Interfaces;

public interface IHasOperation
{
	public Operation OperationType { get; set; }
}