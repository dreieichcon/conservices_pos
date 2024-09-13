using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Tss;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.Fiskaly.Request.Tss;
using Innkeep.Api.Models.Fiskaly.Response;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyTssRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractFiskalyRepository(authenticationService), IFiskalyTssRepository
{
	public async Task<IHttpResponse<IEnumerable<FiskalyTss>>> GetAll()
	{
		var endpoint = new FiskalyEndpointBuilder().WithTss().Build();

		var result = await Get(endpoint);

		var response = DeserializeResult<FiskalyListResponse<FiskalyTss>>(result);

		return HttpResponse<IEnumerable<FiskalyTss>>.FromResult(response, x => x.Data);
	}

	public Task<FiskalyTss> GetOne(string id) => throw new NotImplementedException();

	public async Task<IHttpResponse<FiskalyTss>> CreateTss(string id)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(id).Build();

		var result = await Put(endpoint, "{}");

		return DeserializeResult<FiskalyTss>(result);
	}

	public async Task<IHttpResponse<FiskalyTss>> DeployTss(FiskalyTss current)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).Build();

		var content = Serialize(
			new FiskalyTssStateRequest
			{
				State = TssState.Uninitialized,
			}
		);

		var result = await Patch(endpoint, content, 30000);

		return DeserializeResult<FiskalyTss>(result);
	}

	public async Task<IHttpResponse<FiskalyTss>> InitializeTss(FiskalyTss current)
	{
		var authResult = await AuthenticateAdmin(current.Id);

		if (!authResult.IsSuccess)
			return HttpResponse<FiskalyTss>.FromUnsuccessfulResponse(authResult);

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).Build();

		var content = Serialize(
			new FiskalyTssStateRequest
			{
				State = TssState.Initialized,
				Description = current.Description,
			}
		);

		var result = await Patch(endpoint, content);

		await LogoutAdmin(current.Id);

		return DeserializeResult<FiskalyTss>(result);
	}

	public async Task<IHttpResponse<bool>> ChangeAdminPin(FiskalyTss current)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).WithAdmin().Build();

		var config = AuthenticationService.CurrentConfig;

		var content = Serialize(
			new FiskalyAdminPinRequest
			{
				AdminPuk = config.TsePuk!,
				NewAdminPin = config.TseAdminPassword!,
			}
		);

		var result = await Patch(endpoint, content);

		return HttpResponse<bool>.Parse(result, result.IsSuccess);
	}
}