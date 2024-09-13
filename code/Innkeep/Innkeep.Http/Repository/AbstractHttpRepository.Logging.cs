using Flurl.Http;
using Serilog;

namespace Lite.Http.Repository;

public abstract partial class AbstractHttpRepository<TPb>
{
	/// <summary>
	///     Defines logging for requests.
	/// </summary>
	/// <param name="response">Message response to convert into a log message.</param>
	private static async Task LogResponse(IFlurlResponse response, string requestUri)
	{
		if (response.StatusCode < 300)
		{
			Log.Debug("Received response with Status {StatusCode} from {Endpoint}", response.StatusCode, requestUri);

			return;
		}

		Log.Error(
			"Request to {Endpoint} failed with status: {StatusCode} \n Error Content: {Content}",
			requestUri,
			response.StatusCode,
			await response.GetStringAsync()
		);
	}
}