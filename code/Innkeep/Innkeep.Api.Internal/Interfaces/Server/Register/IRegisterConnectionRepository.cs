using Demolite.Http.Interfaces;

namespace Innkeep.Api.Internal.Interfaces.Server.Register;

public interface IRegisterConnectionRepository
{
	public Task<IHttpResponse<bool>> Test();

	public Task<IHttpResponse<bool>> Connect(string identifier, string description, string hostName);

	public Task<IHttpResponse<bool>> Discover(string address);
}