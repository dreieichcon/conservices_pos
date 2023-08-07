using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces;

public interface IBaseRepository<T> where T : class
{
	public DbContext CreateContext();
	
	public T? Get();

	public T? GetCustom(Func<T, bool> match);

	public IEnumerable<T> GetAll();

	public IEnumerable<T> GetAllCustom(Func<T, bool> match);

	public bool Update(T item);

	public bool Create(T item);

	public bool Delete(T item);
}