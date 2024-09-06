using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Api.Models.Fiskaly.Request.Client;
using Innkeep.Api.Models.Fiskaly.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyClientRepository(IFiskalyAuthenticationService authenticationService)
	: Abstract(authenticationService), IFiskalyClientRepository
{
	public async Task<IEnumerable<FiskalyClient>> GetAll(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithClient().Build();

		var result = await Get(endpoint);

		if (!result.IsSuccess)
			return new List<FiskalyClient>();

		var deserialized = Deserialize<FiskalyListResponse<FiskalyClient>>(result.Content);

		return deserialized != null ? deserialized.Data : new List<FiskalyClient>();
	}

	public async Task<FiskalyClient?> GetOne(string tssId, string id)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var result = await Get(endpoint);

		return DeserializeOrNull<FiskalyClient>(result);
	}

	public async Task<FiskalyClient?> CreateClient(string tssId, string id, string serialNumber)
	{
		await AuthenticateAdmin(tssId);

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var content = Serialize(
			new FiskalyClientCreateRequest
			{
				SerialNumber = serialNumber,
			}
		);

		var result = await Put(endpoint, content);

		await LogoutAdmin(tssId);

		return DeserializeOrNull<FiskalyClient>(result);
	}

	public async Task<FiskalyClient?> UpdateClient(string tssId, string id, ClientState state)
	{
		await AuthenticateAdmin(tssId);

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var content = Serialize(
			new FiskalyClientUpdateRequest
			{
				State = state,
			}
		);

		var result = await Patch(endpoint, content);

		await LogoutAdmin(tssId);

		return DeserializeOrNull<FiskalyClient>(result);
	}
}