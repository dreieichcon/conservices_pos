using System.Net;

namespace Innkeep.Http.Interfaces;

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
	///     Response or exception content.
	/// </summary>
	public string Content { get; set; }

	/// <summary>
	///     Whether the total response is successful or not.
	/// </summary>
	public bool IsSuccess { get; set; }
}