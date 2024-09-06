using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Api.Models.Fiskaly.Request.Client;
using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyClientRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractFiskalyRepository(authenticationService), IFiskalyClientRepository
{
	public async Task<IHttpResponse<IEnumerable<FiskalyClient>>> GetAll(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithClient().Build();

		var result = await Get(endpoint);

		var response = DeserializeResult<FiskalyListResponse<FiskalyClient>>(result);

		return HttpResponse<IEnumerable<FiskalyClient>>.FromResponse(response, x => x.Data);
	}

	public async Task<IHttpResponse<FiskalyClient>> GetOne(string tssId, string id)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var result = await Get(endpoint);

		return DeserializeResult<FiskalyClient>(result);
	}

	public async Task<IHttpResponse<FiskalyClient>> CreateClient(string tssId, string id, string serialNumber)
	{
		var authenticationResult = await AuthenticateAdmin(tssId);

		if (!authenticationResult.IsSuccess)
			return HttpResponse<FiskalyClient>.FromUnsuccessfulResponse(authenticationResult);

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var content = Serialize(
			new FiskalyClientCreateRequest
			{
				SerialNumber = serialNumber,
			}
		);

		var result = await Put(endpoint, content);

		await LogoutAdmin(tssId);

		return DeserializeResult<FiskalyClient>(result);
	}

	public async Task<IHttpResponse<FiskalyClient>> UpdateClient(string tssId, string id, ClientState state)
	{
		var authenticationResult = await AuthenticateAdmin(tssId);

		if (!authenticationResult.IsSuccess)
			return HttpResponse<FiskalyClient>.FromUnsuccessfulResponse(authenticationResult);

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(tssId).WithSpecificClient(id).Build();

		var content = Serialize(
			new FiskalyClientUpdateRequest
			{
				State = state,
			}
		);

		var result = await Patch(endpoint, content);

		await LogoutAdmin(tssId);

		return DeserializeResult<FiskalyClient>(result);
	}
}