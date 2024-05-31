using System.Net;
using Innkeep.Api.Enum;

namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
    protected abstract Task PrepareRequest();
    
    private async Task<HttpResponseMessage> SendRequest(RequestType requestType, string uri, string content)
    {
        HttpResponseMessage response;

        await PrepareRequest();
        
        switch (requestType)
        {
            case RequestType.Get:
                var get = CreateGetMessage(uri);
                InitializeGetHeaders(get);
                response = await Client.SendAsync(get);
                break;
            
            case RequestType.Post:
                var post = CreatePostMessage(content);
                InitializePostHeaders();
                response = await Client.PostAsync(uri, post);
                break;
            
            case RequestType.Put:
                var put = CreatePutMessage(content);
                InitializePutHeaders();
                response = await Client.PutAsync(uri, put);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
        }

        await LogResponse(response);
        return response;
    }
    
    protected async Task<string?> Get(string uri)
    {
        var response = await SendRequest(RequestType.Get, uri, string.Empty);
        
        if (response.StatusCode is HttpStatusCode.OK)
            return await response.Content.ReadAsStringAsync();

        return null;
    }

    protected async Task<string?> Post(string uri, string content)
    {
        var response = await SendRequest(RequestType.Post, uri, content);
        
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }

    protected async Task<string?> Put(string uri, string content)
    {
        var response = await SendRequest(RequestType.Put, uri, content);
        
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();

        return null;
    }
}