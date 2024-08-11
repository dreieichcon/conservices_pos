using Serilog;

namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
    private static async Task LogResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            Log.Debug("Received response with Status {StatusCode} from {Endpoint}", response.StatusCode, response.RequestMessage?.RequestUri);
            return;
        }
        
        Log.Error("Request to {Endpoint} failed with status: {StatusCode}", response.RequestMessage?.RequestUri, response.StatusCode);
    }
}