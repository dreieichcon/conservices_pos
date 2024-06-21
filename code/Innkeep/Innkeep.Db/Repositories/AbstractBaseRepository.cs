using System.Linq.Expressions;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Result;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Repositories;

public abstract partial class AbstractBaseRepository<T, TContext> : IAbstractBaseRepository<T>
    where T: class, IHasOperation
    where TContext : DbContext
{
    protected abstract TContext GetContext();
    
    public T? Get(string id)
    {
        using var db = GetContext();
        return db.Set<T>().Find(id);
    }

    private static IDbResult<T> TrySave(TContext db, T item)
    {
        try
        {
            db.SaveChanges();
            return DbResult<T>.Ok(item);
        }
        catch (Exception ex)
        {
            return DbResult<T>.Failed(item, ex.Message);
        }
    }

    ///<inheritdoc/>
    public T? GetCustom(Func<T, bool> match)
    {
        using var db = GetContext();
        return db.Set<T>().AsEnumerable().FirstOrDefault(match.Invoke);
    }

    ///<inheritdoc/>
    public T[] GetAll()
    {
        using var db = GetContext();
        return db.Set<T>().ToArray();
    }

    ///<inheritdoc/>
    public T[] GetAllCustom(Expression<Func<T, bool>> match)
    {
        using var db = GetContext();
        return db.Set<T>().Where(match).ToArray();
    }
    
    ///<inheritdoc/>
    public IDbResult<T>[] CrudMany(IEnumerable<T> items)
    {
        return items.Select(Crud).ToArray();
    }
    
    ///<inheritdoc/>
    public IDbResult<T> Crud(T item)
    {
        return item.OperationType switch
        {
            Operation.Created => Create(item),
            Operation.Updated => Update(item),
            Operation.Removed => Delete(item),
            Operation.None => DbResult<T>.Ok(item),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    ///<inheritdoc/>
    public IDbResult<T> Update(T item)
    {
        using var db = GetContext();
        db.Set<T>().Update(item);
        return TrySave(db, item);
    }
    
    ///<inheritdoc/>
    public IDbResult<T> Create(T item)
    {
        using var db = GetContext();
        db.Set<T>().Add(item);
        return TrySave(db, item);
    }
    
    ///<inheritdoc/>
    public IDbResult<T> Delete(T item)
    {
        using var db = GetContext();
        db.Set<T>().Remove(item);
        return TrySave(db, item);
    }
}