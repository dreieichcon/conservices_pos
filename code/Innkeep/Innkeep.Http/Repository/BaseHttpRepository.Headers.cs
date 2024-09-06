namespace Innkeep.Http.Repository;

public abstract partial class BaseHttpRepository
{
	/// <summary>
	///     Initializes required GET Headers for a request message.
	/// </summary>
	/// <param name="message">Message to attach headers to.</param>
	protected abstract void InitializeGetHeaders(HttpRequestMessage message);

	/// <summary>
	///     Initializes headers for a POST message.
	/// </summary>
	protected abstract void InitializePostHeaders();

	/// <summary>
	///     Initializes headers for a PUT message.
	///     Rarely differs from <see cref="InitializePostHeaders" /> which is why it uses the method in the base
	///     implementation.
	/// </summary>
	protected virtual void InitializePutHeaders() => InitializePostHeaders();

	/// <summary>
	///     Initializes headers for a PATCH message.
	///     Rarely differs from <see cref="InitializePostHeaders" /> which is why it uses the method in the base
	///     implementation.
	/// </summary>
	protected virtual void InitializePatchHeaders() => InitializePostHeaders();
}