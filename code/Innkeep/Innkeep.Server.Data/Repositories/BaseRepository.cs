using Innkeep.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Innkeep.Server.Data.Repositories;

public class BaseRepository<T> where T : class
{

	public DbContext CreateContext()
	{
		return InnkeepServerContext.Create();
	}

	protected static DbSet<T> GetDbSetFromContext(DbContext db)
	{
		return db.Set<T>();
	}

	protected static bool TrySave(DbContext db)
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

	public T? Get()
	{
		using var db = InnkeepServerContext.Create();
		return GetDbSetFromContext(db).FirstOrDefault();
	}

	public T? GetCustom(Func<T, bool> match)
	{
		using var db = InnkeepServerContext.Create();
		return GetDbSetFromContext(db).AsEnumerable().FirstOrDefault(match.Invoke);
	}

	public IEnumerable<T> GetAll()
	{
		using var db = InnkeepServerContext.Create();
		return GetDbSetFromContext(db).ToArray();
	}

	public IEnumerable<T> GetAllCustom(Func<T, bool> match)
	{
		using var db = InnkeepServerContext.Create();
		return GetDbSetFromContext(db).AsEnumerable().Where(match.Invoke).ToArray();
	}

	public bool Update(T item)
	{
		using var db = InnkeepServerContext.Create();

		var set = GetDbSetFromContext(db);

		set.Update(item);

		return TrySave(db);
	}

	public bool Create(T item)
	{
		using var db = InnkeepServerContext.Create();
		var set = GetDbSetFromContext(db);

		set.Add(item);

		return TrySave(db);
	}

	public bool Delete(T item)
	{
		using var db = InnkeepServerContext.Create();
		var set = GetDbSetFromContext(db);

		set.Remove(item);

		return TrySave(db);
	}
}