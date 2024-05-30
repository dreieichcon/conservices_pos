namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
    protected HttpClient Client = new();

    private static HttpRequestMessage CreateGetMessage(string uri) 
        => new(HttpMethod.Get, uri);

    protected abstract HttpContent CreatePostMessage(string content);

    protected abstract HttpContent CreatePutMessage(string content);

}