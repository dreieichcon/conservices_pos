using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Api.Models.Fiskaly.Request.Client;
using Innkeep.Api.Models.Fiskaly.Response;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyClientRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractFiskalyRepository(authenticationService), IFiskalyClientRepository
{
	public async Task<IHttpResponse<IEnumerable<FiskalyClient>>> GetAll(string tssId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).Client();

		var result = await Get<FiskalyListResponse<FiskalyClient>>(uri);

		return HttpResponse<IEnumerable<FiskalyClient>>.FromResult(result, x => x.Data);
	}

	public async Task<IHttpResponse<FiskalyClient>> GetOne(string tssId, string clientId)
	{
		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).Client(clientId);

		return await Get<FiskalyClient>(uri);
	}

	public async Task<IHttpResponse<FiskalyClient>> CreateClient(string tssId, string clientId, string serialNumber)
	{
		var authenticationResult = await AuthenticateAdmin(tssId);

		if (!authenticationResult.IsSuccess)
			return HttpResponse<FiskalyClient>.FromResult(authenticationResult, _ => null);

		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).Client(clientId);

		var payload = new FiskalyClientCreateRequest
		{
			SerialNumber = serialNumber,
		};

		var result = await Put<FiskalyClientCreateRequest, FiskalyClient>(uri, payload);

		await LogoutAdmin(tssId);

		return result;
	}

	public async Task<IHttpResponse<FiskalyClient>> UpdateClient(string tssId, string clientId, ClientState state)
	{
		var authenticationResult = await AuthenticateAdmin(tssId);

		if (!authenticationResult.IsSuccess)
			return HttpResponse<FiskalyClient>.FromResult(authenticationResult, _ => null);

		var uri = FiskalyUrlBuilder.Endpoints.Tss(tssId).Client(clientId);

		var payload = new FiskalyClientUpdateRequest
		{
			State = state,
		};

		var result = await Patch<FiskalyClientUpdateRequest, FiskalyClient>(uri, payload);

		await LogoutAdmin(tssId);

		return result;
	}
}