namespace Innkeep.Db.Interfaces;

public interface IDbResult<T>
{
	public T Item { get; set; }
    
	public bool Success { get; set; }
    
	public string ErrorMessage { get; set; }
}