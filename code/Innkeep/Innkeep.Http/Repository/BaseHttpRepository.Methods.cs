using Innkeep.Http.Enum;
using Innkeep.Http.Response;

namespace Innkeep.Http.Repository;

public abstract partial class BaseHttpRepository
{
	/// <summary>
	///     Default request timeout in milliseconds (5 seconds).
	///     Can be overridden to change the default request timeout.
	/// </summary>
	protected virtual int Timeout => 5000;

	/// <summary>
	///     Method used to prepare before a request is executed.
	///     Can be used to check token validity or other tasks.
	/// </summary>
	/// <returns>Nothing.</returns>
	protected abstract Task PrepareRequest();

	/// <summary>
	///     Used to override the HttpClient Timeout with a new value.
	/// </summary>
	/// <param name="timeout">Timeout in milliseconds.</param>
	private void SetTimeout(int? timeout)
	{
		Client.Timeout = TimeSpan.FromMilliseconds(timeout ?? Timeout);
	}

	/// <summary>
	///     Method which prepares the request and uses the according request method to send the request.
	/// </summary>
	/// <param name="requestType">Request type.</param>
	/// <param name="uri">Target uri.</param>
	/// <param name="content">Request content.</param>
	/// <param name="timeout">Request timeout, or null to use default <see cref="Timeout" />.</param>
	/// <returns>A HttpResponseMessage containing the result.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the request type has not been implemented.</exception>
	private async Task<HttpResponseMessage> SendRequest(
		RequestType requestType,
		string uri,
		string content,
		int? timeout = null
	)
	{
		HttpResponseMessage response;

		await PrepareRequest();

		SetTimeout(timeout);

		switch (requestType)
		{
			case RequestType.Get:
				var get = CreateGetMessage(uri);
				InitializeGetHeaders(get);
				response = await Client.SendAsync(get);

				break;

			case RequestType.Post:
				var post = CreatePostMessage(content);
				InitializePostHeaders();
				response = await Client.PostAsync(uri, post);

				break;

			case RequestType.Patch:
				var patch = CreatePatchMessage(content);
				InitializePatchHeaders();
				response = await Client.PatchAsync(uri, patch);

				break;

			case RequestType.Put:
				var put = CreatePutMessage(content);
				InitializePutHeaders();
				response = await Client.PutAsync(uri, put);

				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
		}

		SetTimeout(Timeout);

		await LogResponse(response);

		return response;
	}

	/// <summary>
	///     Sends a GET request to a specified uri.
	/// </summary>
	/// <param name="uri">Target uri.</param>
	/// <returns>An <see cref="ApiResponse" /> object containing the request result.</returns>
	protected async Task<ApiResponse> Get(string uri)
	{
		var response = await SendRequest(RequestType.Get, uri, string.Empty);

		return await ApiResponse.FromResponse(response);
	}

	/// <summary>
	///     Sends a GET request with a specific timeout value to a specified uri.
	/// </summary>
	/// <param name="uri">Target uri.</param>
	/// <param name="timeout">Timeout in milliseconds.</param>
	/// <returns>An <see cref="ApiResponse" /> object containing the request result.</returns>
	protected async Task<ApiResponse> Get(string uri, int timeout)
	{
		var response = await SendRequest(RequestType.Get, uri, string.Empty, timeout);

		return await ApiResponse.FromResponse(response);
	}

	/// <summary>
	///     Sends a GET request with Form content to a specified uri.
	/// </summary>
	/// <param name="uri">Target uri.</param>
	/// <param name="formContent">Form content.</param>
	/// <returns>An <see cref="ApiResponse" /> object containing the request result.</returns>
	protected async Task<ApiResponse> Get(string uri, Dictionary<string, string> formContent)
	{
		var get = CreateGetMessage(uri);
		InitializeGetHeaders(get);
		get.Content = new FormUrlEncodedContent(formContent);
		var response = await Client.SendAsync(get);

		await LogResponse(response);

		return await ApiResponse.FromResponse(response);
	}

	/// <summary>
	///     Sends a POST request to a specified uri.
	/// </summary>
	/// <param name="uri">Target uri.</param>
	/// <param name="content">Request json.</param>
	/// <returns>An <see cref="ApiResponse" /> object containing the request result.</returns>
	protected async Task<ApiResponse> Post(string uri, string content)
	{
		var response = await SendRequest(RequestType.Post, uri, content);

		return await ApiResponse.FromResponse(response);
	}

	/// <summary>
	///     Sends a PUT request to a specified uri.
	/// </summary>
	/// <param name="uri">Target uri.</param>
	/// <param name="content">Request json.</param>
	/// <returns>An <see cref="ApiResponse" /> object containing the request result.</returns>
	protected async Task<ApiResponse> Put(string uri, string content)
	{
		var response = await SendRequest(RequestType.Put, uri, content);

		return await ApiResponse.FromResponse(response);
	}

	protected async Task<ApiResponse> Patch(string uri, string content, int? timeout = null)
	{
		var response = await SendRequest(RequestType.Patch, uri, content, timeout);

		return await ApiResponse.FromResponse(response);
	}
}