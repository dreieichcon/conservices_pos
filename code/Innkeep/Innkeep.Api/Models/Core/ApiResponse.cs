using System.Net;

namespace Innkeep.Api.Models.Core;

public class ApiResponse
{
	public string Content { get; set; } = string.Empty;
	
	public HttpStatusCode Code { get; set; }
	
	public bool IsSuccess { get; set; }

	public static async Task<ApiResponse> FromResponse(HttpResponseMessage responseMessage)
	{
		return new ApiResponse()
		{
			Content = await responseMessage.Content.ReadAsStringAsync(),
			IsSuccess = responseMessage.IsSuccessStatusCode,
			Code = responseMessage.StatusCode,
		};
	}
}