using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects;
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
}