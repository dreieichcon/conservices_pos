using System.Linq.Expressions;
using Innkeep.Db.Enum;

namespace Innkeep.Db.Interfaces;

public interface IAbstractBaseRepository<T>
{
    public T? Get(string id);
    
    public Task<T?> GetAsync(string id);
    
    /// <summary>
    /// Tries to find an item in the dataset via a custom matching function.
    /// </summary>
    /// <param name="match">Matching function.</param>
    /// <returns>The item if found, else null.</returns>
    public T? GetCustom(Func<T, bool> match);

    /// <summary>
    /// Tries to find an item in the dataset via a custom matching function.
    /// </summary>
    /// <param name="match">Matching function.</param>
    /// <returns>The item if found, else null.</returns>
    public Task<T?> GetCustomAsync(Func<T, bool> match);

    /// <summary>
    /// Retrieves all items from the dataset.
    /// </summary>
    /// <returns>An array with all items, or an empty array if none exist.</returns>
    public T[] GetAll();
    
    /// <summary>
    /// Retrieves all items from the dataset.
    /// </summary>
    /// <returns>An array with all items, or an empty array if none exist.</returns>
    public Task<T[]> GetAllAsync();

    /// <summary>
    /// Retrieves an array of items via a custom matching function.
    /// </summary>
    /// <param name="match">Matching function.</param>
    /// <returns>All items which match, or an empty array if none exist.</returns>
    public T[] GetAllCustom(Expression<Func<T, bool>> match);
    
    /// <summary>
    /// Retrieves an array of items via a custom matching function.
    /// </summary>
    /// <param name="match">Matching function.</param>
    /// <returns>All items which match, or an empty array if none exist.</returns>
    public Task<T[]> GetAllCustomAsync(Expression<Func<T, bool>> match);
    
    /// <summary>
    /// Applies a Crud operation based on the <see cref="Operation"/> of an item.
    /// Invokes either <see cref="Create"/>, <see cref="Update"/>, or <see cref="Delete"/>
    /// </summary>
    /// <param name="item">Item which will be processed.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public IDbResult<T> Crud(T item);
    
    /// <summary>
    /// Applies a Crud operation based on the <see cref="Operation"/> of an item.
    /// Invokes either <see cref="CreateAsync"/>, <see cref="UpdateAsync"/>, or <see cref="DeleteAsync"/>
    /// </summary>
    /// <param name="item">Item which will be processed.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public Task<IDbResult<T>> CrudAsync(T item);

    /// <summary>
    /// Applies the <see cref="Crud"/> method to all items in the collection.
    /// This method calls SaveChanges for every single item.
    /// This means every save which does not fail is committed to the database.
    /// </summary>
    /// <param name="items">Items which will be processed.</param>
    /// <returns>An array of <see cref="IDbResult{T}"/> based on the success status.</returns>
    public IDbResult<T>[] CrudMany(IEnumerable<T> items);

    /// <summary>
    /// Applies the <see cref="CrudAsync"/> method to all items in the collection.
    /// This method calls SaveChanges for every single item.
    /// This means every save which does not fail is committed to the database.
    /// </summary>
    /// <param name="items">Items which will be processed.</param>
    /// <returns>An array of <see cref="IDbResult{T}"/> based on the success status.</returns>
    public Task<IDbResult<T>[]> CrudManyAsync(IEnumerable<T> items);

    /// <summary>
    /// Creates an item in the database.
    /// </summary>
    /// <param name="item">Item to create.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public IDbResult<T> Create(T item);

    /// <summary>
    /// Creates an item in the database.
    /// </summary>
    /// <param name="item">Item to create.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public Task<IDbResult<T>> CreateAsync(T item);
    
    /// <summary>
    /// Updates an item in the database.
    /// </summary>
    /// <param name="item">Item to update.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public IDbResult<T> Update(T item);
    
    /// <summary>
    /// Updates an item in the database.
    /// </summary>
    /// <param name="item">Item to update.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public Task<IDbResult<T>> UpdateAsync(T item);
    
    /// <summary>
    /// Deletes an item from the database.
    /// </summary>
    /// <param name="item">Item to delete.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public IDbResult<T> Delete(T item);
    
    /// <summary>
    /// Deletes an item from the database.
    /// </summary>
    /// <param name="item">Item to delete.</param>
    /// <returns>An <see cref="IDbResult{T}"/> based on the success status.</returns>
    public Task<IDbResult<T>> DeleteAsync(T item);
}