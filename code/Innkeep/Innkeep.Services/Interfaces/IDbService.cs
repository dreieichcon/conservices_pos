namespace Innkeep.Services.Interfaces;

public interface IDbService<T>
{
	public event EventHandler? ItemsUpdated;
	
	public T CurrentItem { get; set; }
	
	public IEnumerable<T> Items { get; set; }

	public Task Load();

	public Task<bool> Save();
}