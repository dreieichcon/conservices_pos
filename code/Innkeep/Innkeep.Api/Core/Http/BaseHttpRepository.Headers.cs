namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
    protected abstract void InitializeGetHeaders(HttpRequestMessage message);
    
    protected abstract void InitializePostHeaders();
    
    protected abstract void InitializePutHeaders();

    protected abstract void InitializePatchHeaders();
}