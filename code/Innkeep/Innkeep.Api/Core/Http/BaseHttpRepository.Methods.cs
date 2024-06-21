using System.Net;
using Innkeep.Api.Enum;
using Innkeep.Api.Models.Core;

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
    
    protected async Task<ApiResponse> Get(string uri)
    {
        var response = await SendRequest(RequestType.Get, uri, string.Empty);
        return await ApiResponse.FromResponse(response);
    }

    protected async Task<ApiResponse> Post(string uri, string content)
    {
        var response = await SendRequest(RequestType.Post, uri, content);
        return await ApiResponse.FromResponse(response);
    }

    protected async Task<ApiResponse> Put(string uri, string content)
    {
        var response = await SendRequest(RequestType.Put, uri, content);
        return await ApiResponse.FromResponse(response);
    }
}