using Flurl.Http;

namespace Lite.Http.Repository;

public abstract partial class AbstractHttpRepository<TPb>
{
	/// <summary>
	///     Attaches headers to a GET request.
	/// </summary>
	/// <param name="request">Request to attach headers to.</param>
	protected abstract void AttachGetHeaders(IFlurlRequest request);

	/// <summary>
	///     Attaches headers to a POST request.
	/// </summary>
	/// <param name="request">Request to attach headers to.</param>
	protected abstract void AttachPostHeaders(IFlurlRequest request);

	/// <summary>
	///     Attaches headers to a PUT request.
	///     Rarely differs from <see cref="AttachPostHeaders" /> which is why it uses the method in the base
	///     implementation.
	/// </summary>
	/// <param name="request">Request to attach headers to.</param>
	protected virtual void AttachPutHeaders(IFlurlRequest request) => AttachPostHeaders(request);

	/// <summary>
	///     Attaches headers to a PATCH request.
	///     Rarely differs from <see cref="AttachPostHeaders" /> which is why it uses the method in the base
	///     implementation.
	/// </summary>
	/// <param name="request">Request to attach headers to.</param>
	protected virtual void AttachPatchHeaders(IFlurlRequest request) => AttachPostHeaders(request);

	/// <summary>
	///     Attaches headers to a DELETE request.
	///     Rarely differs from <see cref="AttachPostHeaders" /> which is why it uses the method in the base
	///     implementation.
	/// </summary>
	/// <param name="request">Request to attach headers to.</param>
	protected virtual void AttachDeleteHeaders(IFlurlRequest request) => AttachPostHeaders(request);
}