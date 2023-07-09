using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Innkeep.Server.Data.Repositories;

public class BaseRepository<T> where T : class
{

	public DbContext CreateContext()
	{
		return InnkeepServerContext.Create();
	}

	private static DbSet<T> GetDbSetFromContext(DbContext db)
	{
		return db.Set<T>();
	}

	private static bool TrySave(DbContext db)
	{
		try
		{
			db.SaveChanges();
			return true;
		}
		catch(Exception ex)
		{
			Log.Error("Error while saving {Type}:{Exception}", typeof(T), ex);
			return false;
		}
	}

	public T? Get(DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		return GetDbSetFromContext(db).FirstOrDefault();
	}

	public T? GetCustom(Func<T, bool> match, DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		return GetDbSetFromContext(db).AsEnumerable().FirstOrDefault(match.Invoke);
	}

	public IEnumerable<T> GetAll(DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		return GetDbSetFromContext(db).ToArray();
	}

	public IEnumerable<T> GetAllCustom(Func<T, bool> match, DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		return GetDbSetFromContext(db).AsEnumerable().Where(match.Invoke).ToArray();
	}

	public bool Update(T item, DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();

		var set = GetDbSetFromContext(db);

		set.Update(item);

		return TrySave(db);
	}

	public bool Create(T item, DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		var set = GetDbSetFromContext(db);

		set.Add(item);

		return TrySave(db);
	}

	public bool Delete(T item, DbContext? db = null)
	{
		db ??= InnkeepServerContext.Create();
		var set = GetDbSetFromContext(db);

		set.Remove(item);

		return TrySave(db);
	}
}