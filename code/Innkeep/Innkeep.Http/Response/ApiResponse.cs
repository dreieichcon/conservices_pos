using System.Net;

namespace Innkeep.Http.Response;

/// <summary>
///     Raw response of an Api Call.
/// </summary>
public class ApiResponse
{
	/// <summary>
	///     The raw response content.
	/// </summary>
	public string Content { get; private init; } = string.Empty;

	/// <summary>
	///     Response status code.
	/// </summary>
	public HttpStatusCode StatusCode { get; private init; }

	/// <summary>
	///     Whether the request was successful or not.
	/// </summary>
	public bool IsSuccess { get; private init; }

	/// <summary>
	///     Creates an object from a response message.
	/// </summary>
	/// <param name="responseMessage"><see cref="HttpResponseMessage" /> to be parsed.</param>
	/// <returns>A populated object of type <see cref="ApiResponse" />.</returns>
	public static async Task<ApiResponse> FromResponse(HttpResponseMessage responseMessage) =>
		new()
		{
			Content = await responseMessage.Content.ReadAsStringAsync(),
			IsSuccess = responseMessage.IsSuccessStatusCode,
			StatusCode = responseMessage.StatusCode,
		};

	/// <summary>
	///     Creates an object from an exception.
	/// </summary>
	/// <param name="exception">Exception to be parsed.</param>
	/// <returns>A populated object of type <see cref="ApiResponse" />.</returns>
	public static ApiResponse FromException(Exception exception) =>
		new()
		{
			Content = exception.Message,
			IsSuccess = false,
			StatusCode = HttpStatusCode.InternalServerError,
		};
}