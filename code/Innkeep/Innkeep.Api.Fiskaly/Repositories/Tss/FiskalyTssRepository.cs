using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Tss;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Api.Models.Fiskaly.Request.Auth;
using Innkeep.Api.Models.Fiskaly.Request.Tss;
using Innkeep.Api.Models.Fiskaly.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Tss;

public class FiskalyTssRepository(IFiskalyAuthenticationService authenticationService)
	: BaseFiskalyRepository(authenticationService), IFiskalyTssRepository
{
	public async Task<IEnumerable<FiskalyTss>> GetAll()
	{
		var endpoint = new FiskalyEndpointBuilder().WithTss().Build();

		var result = await Get(endpoint);

		if (!result.IsSuccess)
			return new List<FiskalyTss>();

		var deserialized = Deserialize<FiskalyListResponse<FiskalyTss>>(result.Content);

		return deserialized != null ? deserialized.Data : new List<FiskalyTss>();
	}

	public Task<FiskalyTss> GetOne(string id) => throw new NotImplementedException();

	public async Task<FiskalyTss?> CreateTss(string id)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(id).Build();

		var result = await Put(endpoint, "{}");

		return DeserializeOrNull<FiskalyTss>(result);
	}

	public async Task<FiskalyTss?> DeployTss(FiskalyTss current)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).Build();

		var content = Serialize(
			new FiskalyTssStateRequest()
			{
				State = TssState.Uninitialized,
			}
		);

		var result = await Patch(endpoint, content, 30000);

		return DeserializeOrNull<FiskalyTss>(result);
	}

	public async Task<FiskalyTss?> InitializeTss(FiskalyTss current)
	{
		var authResult = await AuthenticateAdmin(current.Id);

		if (!authResult) return null;

		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).Build();

		var content = Serialize(
			new FiskalyTssStateRequest()
			{
				State = TssState.Initialized,
				Description = current.Description,
			}
		);

		var result = await Patch(endpoint, content);

		await LogoutAdmin(current.Id);

		return DeserializeOrNull<FiskalyTss>(result);
	}

	public async Task<bool> ChangeAdminPin(FiskalyTss current)
	{
		var endpoint = new FiskalyEndpointBuilder().WithSpecificTss(current.Id).WithAdmin().Build();

		var config = AuthenticationService.CurrentConfig;

		var content = Serialize(
			new FiskalyAdminPinRequest()
			{
				AdminPuk = config.TsePuk!,
				NewAdminPin = config.TseAdminPassword!,
			}
		);

		var result = await Patch(endpoint, content);

		return result.IsSuccess;
	}
}