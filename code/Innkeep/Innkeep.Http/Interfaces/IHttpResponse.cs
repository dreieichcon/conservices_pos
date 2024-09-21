using System.Net;

namespace Lite.Http.Interfaces;

public interface IHttpResponse<T> : IHttpResponse
{
	/// <summary>
	///     Deserialized object.
	/// </summary>
	public T? Object { get; set; }
}

public interface IHttpResponse
{
	/// <summary>
	///     Request status code.
	/// </summary>
	public HttpStatusCode StatusCode { get; set; }

	/// <summary>
	///     Response content.
	/// </summary>
	public string Content { get; set; }

	/// <summary>
	///     Exception content.
	/// </summary>
	public string ErrorContent { get; set; }

	/// <summary>
	///     Whether the total response is successful or not.
	/// </summary>
	public bool IsSuccess { get; set; }
}