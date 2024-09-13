using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Fiskaly.Interfaces.Tss;

public interface IFiskalyClientRepository
{
	public Task<IHttpResponse<IEnumerable<FiskalyClient>>> GetAll(string tssId);

	public Task<IHttpResponse<FiskalyClient>> GetOne(string tssId, string id);

	public Task<IHttpResponse<FiskalyClient>> CreateClient(string tssId, string id, string serialNumber);

	public Task<IHttpResponse<FiskalyClient>> UpdateClient(string tssId, string id, ClientState state);
}