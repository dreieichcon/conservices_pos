namespace Innkeep.Db.Interfaces;

public interface IDbItem : IHasOperation
{
	public string Id { get; set; }
}