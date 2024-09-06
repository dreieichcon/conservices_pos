using Serilog;

namespace Innkeep.Http.Repository;

public abstract partial class BaseHttpRepository
{
	/// <summary>
	///     Defines logging for requests.
	/// </summary>
	/// <param name="response">Message response to convert into a log message.</param>
	private static async Task LogResponse(HttpResponseMessage response)
	{
		if (response.IsSuccessStatusCode)
		{
			Log.Debug(
				"Received response with Status {StatusCode} from {Endpoint}",
				response.StatusCode,
				response.RequestMessage?.RequestUri
			);

			return;
		}

		Log.Error(
			"Request to {Endpoint} failed with status: {StatusCode} \n Error Content: {Content}",
			response.RequestMessage?.RequestUri,
			response.StatusCode,
			await response.Content.ReadAsStringAsync()
		);
	}
}