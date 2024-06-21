using Innkeep.Db.Interfaces;

namespace Innkeep.Db.Result;

public class DbResult<T> : IDbResult<T>
{
	private DbResult(T item, bool success, string errorMessage)
	{
		Item = item;
		Success = success;
		ErrorMessage = errorMessage;
	}

	/// <summary>
	/// Creates a <see cref="DbResult{T}"/> with the status ok.
	/// </summary>
	/// <param name="item">Modified item.</param>
	/// <returns>A new <see cref="DbResult{T}"/> with the status ok.</returns>
	public static DbResult<T> Ok(T item)
	{
		return new DbResult<T>(item, true, string.Empty);
	}

	/// <summary>
	/// Creates a <see cref="DbResult{T}"/> with the status failed.
	/// </summary>
	/// <param name="item">Modified item.</param>
	/// <param name="errorMessage">The exception message.</param>
	/// <returns>A new <see cref="DbResult{T}"/> with the status failed.</returns>
	public static DbResult<T> Failed(T item, string errorMessage)
	{
		return new DbResult<T>(item, false, errorMessage);
	}

	/// <summary>
	/// The modified item.
	/// </summary>
	public T Item { get; set; }

	/// <summary>
	/// Whether the result was successful or not.
	/// </summary>
	public bool Success { get; set; }

	/// <summary>
	/// The error message, if an error has occurred.
	/// </summary>
	public string ErrorMessage { get; set; }
}