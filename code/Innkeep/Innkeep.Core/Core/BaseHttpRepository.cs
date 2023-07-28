using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using RestSharp;

namespace Innkeep.Core.Core;

public abstract class BaseHttpRepository
{
    protected HttpClient Client;
    
    public BaseHttpRepository()
    {
        Client = new HttpClient();
    }

    protected HttpRequestMessage CreateGetMessage(string endpoint)
    {
        return new HttpRequestMessage(HttpMethod.Get, endpoint);
    }
    
	protected async Task<string> ExecuteGetRequest(HttpRequestMessage message)
    {
        PrepareGetHeaders(message);
        var response = await Client.SendAsync(message);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            throw new HttpRequestException(response.StatusCode.ToString());
        }
    }
    
    protected async Task<string> ExecutePostRequest(string endpoint, HttpContent jsonContent)
    {
        PreparePostHeaders();

        var response = await Client.PostAsync(endpoint, jsonContent);

        if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            var debug = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(debug);
        }
    }

    protected abstract void PrepareGetHeaders(HttpRequestMessage message);

    protected abstract void PreparePostHeaders();

    protected void ConnectionLog(HttpRequestMessage message)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Method: {message.Method}");
        
        sb.AppendLine($"Destinaton: {message.RequestUri}");
        
        sb.AppendLine("Headers:");
        
        foreach (var parameter in message.Headers)
        {
            sb.AppendLine($"{parameter.Key}: {parameter.Value.First()}");
        }
        
        Trace.WriteLine(sb.ToString());
    }

    protected void RequestLog(RestResponse response)
    {
        var sb = new StringBuilder();

        sb.AppendLine(new string('-', 50));

        foreach (var header in response.Headers)
        {
            sb.AppendLine($"{header.Name}: {header.Value}");
        }

        Trace.WriteLine(sb.ToString());
    }
}