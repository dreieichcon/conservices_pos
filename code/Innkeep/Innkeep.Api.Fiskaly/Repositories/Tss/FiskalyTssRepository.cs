using Flurl.Http;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Enum.Fiskaly.Tss;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.Fiskaly.Request.Tss;
using Innkeep.Api.Models.Fiskaly.Response;
using Innkeep.Api.Models.General;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyTssRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractFiskalyRepository(authenticationService), IFiskalyTssRepository
{
	protected override int Timeout => 30000;

	public async Task<IHttpResponse<IEnumerable<FiskalyTss>>> GetAll()
	{
		var uri = FiskalyUrlBuilder.Endpoints.Tss;
		var result = await Get<FiskalyListResponse<FiskalyTss>>(uri);

		return HttpResponse<IEnumerable<FiskalyTss>>.FromResult(result, x => x.Data);
	}

	public Task<FiskalyTss> GetOne(string id) => throw new NotImplementedException();

	public async Task<IHttpResponse<FiskalyTss>> CreateTss(string id)
	{
		var uri = FiskalyUrlBuilder.Endpoints.SpecificTss(id);

		return await Put<Empty, FiskalyTss>(uri, new Empty());
	}

	public async Task<IHttpResponse<FiskalyTss>> DeployTss(FiskalyTss current)
	{
		var endpoint = FiskalyUrlBuilder.Endpoints.SpecificTss(current.Id);

		var payload = new FiskalyTssStateRequest
		{
			State = TssState.Uninitialized,
		};

		return await Patch<FiskalyTssStateRequest, FiskalyTss>(endpoint, payload);
	}

	public async Task<IHttpResponse<FiskalyTss>> InitializeTss(FiskalyTss current)
	{
		var authResult = await AuthenticateAdmin(current.Id);

		if (!authResult.IsSuccess)
			return HttpResponse<FiskalyTss>.FromResult(authResult, _ => null);

		var endpoint = FiskalyUrlBuilder.Endpoints.SpecificTss(current.Id);

		var payload = new FiskalyTssStateRequest
		{
			State = TssState.Initialized,
			Description = current.Description,
		};

		var result = await Patch<FiskalyTssStateRequest, FiskalyTss>(endpoint, payload);

		await LogoutAdmin(current.Id);

		return result;
	}

	public async Task<IHttpResponse<bool>> ChangeAdminPin(FiskalyTss current)
	{
		var uri = FiskalyUrlBuilder.Endpoints.SpecificTss(current.Id).Admin;

		var config = AuthenticationService.CurrentConfig;

		var payload = new FiskalyAdminPinRequest
		{
			AdminPuk = config.TsePuk!,
			NewAdminPin = config.TseAdminPassword!,
		};

		var result = await Patch<FiskalyAdminPinRequest, Empty>(uri, payload);

		return HttpResponse<bool>.FromResult(result, _ => result.IsSuccess);
	}

	protected override IFlurlRequest CreateRequest(IUrlBuilder<FiskalyParameterBuilder> urlBuilder)
	{
		var request = base.CreateRequest(urlBuilder);
		request.WithTimeout(TimeSpan.FromMilliseconds(Timeout));
		return request;
	}
}