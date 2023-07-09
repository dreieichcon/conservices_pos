using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces;

public interface IBaseRepository<T> where T : class
{
	public DbContext CreateContext();
	
	public T? Get(DbContext? db = null);

	public T? GetCustom(Func<T, bool> match, DbContext? db = null);

	public IEnumerable<T> GetAll(DbContext? db = null);

	public IEnumerable<T> GetAllCustom(Func<T, bool> match, DbContext? db = null);

	public bool Update(T item, DbContext? db = null);

	public bool Create(T item, DbContext? db = null);

	public bool Delete(T item, DbContext? db = null);
}