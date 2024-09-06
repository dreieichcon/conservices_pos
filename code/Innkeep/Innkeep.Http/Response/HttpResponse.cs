using System.Net;
using Innkeep.Http.Interfaces;

namespace Innkeep.Http.Response;

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

	public static HttpResponse<T> FromUnsuccessfulResponse(IHttpResponse response) =>
		new()
		{
			StatusCode = response.StatusCode,
			Content = response.Content,
			IsSuccess = false,
		};

	public static HttpResponse<T> Parse(ApiResponse response, T? @object) =>
		response.IsSuccess ? Ok(response, @object) : Error(response);

	public static HttpResponse<T> Ok(ApiResponse response, T? @object) =>
		new()
		{
			StatusCode = response.StatusCode,
			Content = response.Content,
			Object = @object,
			IsSuccess = true,
		};

	public static HttpResponse<T> Error(ApiResponse response) => new()
	{
		StatusCode = response.StatusCode,
		Content = response.Content,
		Object = default,
		IsSuccess = false,
	};

	public static HttpResponse<T> Exception(Exception ex) => new()
	{
		Content = ex.Message,
		IsSuccess = false,
	};
}