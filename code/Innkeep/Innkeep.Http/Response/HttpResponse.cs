using System.Net;
using Flurl.Http;
using Lite.Http.Interfaces;

namespace Lite.Http.Response;

public class HttpResponse<T> : IHttpResponse<T>
{
	/// <inheritdoc />
	public HttpStatusCode StatusCode { get; set; }

	/// <inheritdoc />
	public string Content { get; set; } = string.Empty;

	/// <inheritdoc />
	public T? Object { get; set; }

	/// <inheritdoc />
	public bool IsSuccess { get; set; }

	public static async Task<HttpResponse<T>> Ok(IFlurlResponse response, T? deserialized)
		=> new()
		{
			StatusCode = (HttpStatusCode) response.StatusCode,
			Content = await response.GetStringAsync(),
			Object = deserialized,
			IsSuccess = true,
		};

	public static async Task<HttpResponse<T>> Error(IFlurlResponse response, T? defaultValue)
		=> new()
		{
			StatusCode = (HttpStatusCode) response.StatusCode,
			Content = await response.GetStringAsync(),
			Object = defaultValue,
			IsSuccess = false,
		};

	public static HttpResponse<T> Exception<T>(Exception exception, T? defaultValue)
		=> new()
		{
			StatusCode = (HttpStatusCode) 500,
			Content = exception.ToString(),
			Object = defaultValue,
			IsSuccess = false,
		};

	public static HttpResponse<T> FromResult<TR>(IHttpResponse<TR> result, Func<TR, T?> func)
	{
		if (result.IsSuccess)
			return new HttpResponse<T>
			{
				StatusCode = result.StatusCode,
				Content = result.Content,
				Object = result.Object != null ? func.Invoke(result.Object) : default,
				IsSuccess = result.IsSuccess,
			};

		return new HttpResponse<T>
		{
			StatusCode = result.StatusCode,
			Content = result.Content,
			IsSuccess = result.IsSuccess,
		};
	}
}