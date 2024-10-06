using Demolite.Http.Interfaces;

namespace Innkeep.Api.Internal.Interfaces.Client.Actions;

public interface IClientReloadRepository
{
	public Task<IHttpResponse<bool>> Reload(string identifier, string address);
}