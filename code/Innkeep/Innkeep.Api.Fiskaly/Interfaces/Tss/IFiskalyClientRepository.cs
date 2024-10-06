using Demolite.Http.Interfaces;
using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Models.Fiskaly.Objects.Client;

namespace Innkeep.Api.Fiskaly.Interfaces.Tss;

public interface IFiskalyClientRepository
{
	public Task<IHttpResponse<IEnumerable<FiskalyClient>>> GetAll(string tssId);

	public Task<IHttpResponse<FiskalyClient>> GetOne(string tssId, string clientId);

	public Task<IHttpResponse<FiskalyClient>> CreateClient(string tssId, string clientId, string serialNumber);

	public Task<IHttpResponse<FiskalyClient>> UpdateClient(string tssId, string clientId, ClientState state);
}