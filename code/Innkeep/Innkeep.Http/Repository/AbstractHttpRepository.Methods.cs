using System.Text;
using System.Text.Json;
using Flurl.Http;
using Lite.Http.Enum;
using Lite.Http.Interfaces;

namespace Lite.Http.Repository;

public abstract partial class AbstractHttpRepository<TPb>
{
	/// <summary>
	///     Method used to prepare before a request is executed.
	///     Can be used to check token validity or other tasks.
	/// </summary>
	/// <returns>Nothing.</returns>
	protected abstract Task PrepareRequest();

	/// <summary>
	///     Executes a GET request.
	/// </summary>
	/// <param name="builder">Url builder.</param>
	/// <param name="formContent">Possible get form content.</param>
	/// <param name="defaultValue">Default return value.</param>
	/// <typeparam name="TR">Return value type.</typeparam>
	/// <returns>A <see cref="IHttpResponse{T}" /> containing the deserialized return value, or default if there was an error</returns>
	protected async Task<IHttpResponse<TR>> Get<TR>(
		IUrlBuilder<TPb> builder,
		TR? formContent = null,
		TR? defaultValue = default
	)
		where TR : class
		=> await SendRequestInternal(builder, RequestType.Get, formContent, defaultValue);

	/// <summary>
	///     Executes a POST request.
	/// </summary>
	/// <param name="builder">Url builder.</param>
	/// <param name="data">Data to be serialized and sent.</param>
	/// <param name="defaultValue">Default return value.</param>
	/// <typeparam name="T">Transmit type.</typeparam>
	/// <typeparam name="TR">Return type.</typeparam>
	/// <returns></returns>
	protected async Task<IHttpResponse<TR>> Post<T, TR>(IUrlBuilder<TPb> builder, T? data, TR? defaultValue = default)
		=> await SendRequestInternal(builder, RequestType.Post, data, defaultValue);

	/// <summary>
	///     Executes a PUT request.
	/// </summary>
	/// <param name="builder">Url builder.</param>
	/// <param name="data">Data to be serialized and sent.</param>
	/// <param name="defaultValue">Default return value.</param>
	/// <typeparam name="T">Transmit type.</typeparam>
	/// <typeparam name="TR">Return type.</typeparam>
	/// <returns></returns>
	protected async Task<IHttpResponse<TR>> Put<T, TR>(IUrlBuilder<TPb> builder, T? data, TR? defaultValue = default)
		=> await SendRequestInternal(builder, RequestType.Put, data, defaultValue);
	
	/// <summary>
	///     Executes a PATCH request.
	/// </summary>
	/// <param name="builder">Url builder.</param>
	/// <param name="data">Data to be serialized and sent.</param>
	/// <param name="defaultValue">Default return value.</param>
	/// <typeparam name="T">Transmit type.</typeparam>
	/// <typeparam name="TR">Return type.</typeparam>
	/// <returns></returns>
	protected async Task<IHttpResponse<TR>> Patch<T, TR>(IUrlBuilder<TPb> builder, T? data, TR? defaultValue = default)
		=> await SendRequestInternal(builder, RequestType.Patch, data, defaultValue);

	/// <summary>
	///     Method with actually sends the request to the endpoint.
	/// </summary>
	/// <param name="request"></param>
	/// <param name="requestType"></param>
	/// <param name="data"></param>
	/// <param name="defaultValue"></param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TR"></typeparam>
	/// <returns></returns>
	protected async Task<IHttpResponse<TR>> SendRequest<T, TR>(
		IFlurlRequest request,
		RequestType requestType,
		T? data,
		TR? defaultValue = default
	)
	{
		var jsonString = JsonSerializer.Serialize(data);
		var jsonData = new StringContent(jsonString, Encoding.UTF8, "application/json");

		var flurlResponse = requestType switch
		{
			RequestType.Get => await request.GetAsync(),
			RequestType.Post => await request.PostAsync(jsonData),
			RequestType.Patch => await request.PatchAsync(jsonData),
			RequestType.Put => await request.PutAsync(jsonData),
			RequestType.Delete => await request.DeleteAsync(),
			_ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null),
		};

		await LogResponse(flurlResponse, request.Url);

		var response = await DeserializeResult(flurlResponse, defaultValue);

		return response;
	}

	private async Task<IHttpResponse<TR>> SendRequestInternal<T, TR>(
		IUrlBuilder<TPb> builder,
		RequestType requestType,
		T? data,
		TR? defaultValue = default
	)
	{
		await PrepareRequest();
		var request = CreateRequest(builder);

		switch (requestType)
		{
			case RequestType.Get:
				AttachGetHeaders(request);
				AttachGetFormContent(request, data);
				break;

			case RequestType.Post:
				AttachPostHeaders(request);
				break;

			case RequestType.Patch:
				AttachPatchHeaders(request);
				break;

			case RequestType.Put:
				AttachPutHeaders(request);
				break;

			case RequestType.Delete:
				AttachDeleteHeaders(request);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
		}

		return await SendRequest(request, requestType, data, defaultValue);
	}
}