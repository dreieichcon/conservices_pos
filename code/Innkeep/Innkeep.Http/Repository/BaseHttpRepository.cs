using System.Text;

namespace Innkeep.Http.Repository;

/// <summary>
///     Core HTTP Repository Class used for interactions with different APIs.
/// </summary>
public abstract partial class BaseHttpRepository
{
	/// <summary>
	///     The core http client for this repository. Should be only instantiated once.
	/// </summary>
	protected HttpClient Client { get; private set; } = new();

	/// <summary>
	///     Prepares a GET request message.
	/// </summary>
	/// <param name="uri">Request Uri.</param>
	/// <returns>A fully formed HttpRequestMessage with the method GET.</returns>
	private static HttpRequestMessage CreateGetMessage(string uri) => new(HttpMethod.Get, uri);

	/// <summary>
	///     Prepares a POST request message with encoding UTF8 and type "application/json".
	/// </summary>
	/// <param name="content">Json content to wrap.</param>
	/// <returns>Prepared <see cref="StringContent" /> for the request.</returns>
	protected virtual HttpContent CreatePostMessage(string content) =>
		new StringContent(content, Encoding.UTF8, "application/json");

	/// <summary>
	///     Prepares a PUT request message with encoding UTF8 and type "application/json".
	///     Rarely differs from <see cref="CreatePostMessage" /> which is why it uses the method in the base implementation.
	/// </summary>
	/// <param name="content">Json content to wrap.</param>
	/// <returns>Prepared <see cref="StringContent" /> for the request.</returns>
	protected virtual HttpContent CreatePutMessage(string content) => CreatePostMessage(content);

	/// <summary>
	///     Prepares a PATCH request message with Encoding UTF8 and type "application/json".
	///     Rarely differs from <see cref="CreatePostMessage" /> which is why it uses the method in the base implementation.
	/// </summary>
	/// <param name="content">Json content to wrap.</param>
	/// <returns>Prepared <see cref="StringContent" /> for the request.</returns>
	protected virtual HttpContent CreatePatchMessage(string content) => CreatePostMessage(content);
}