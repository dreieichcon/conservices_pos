using Demolite.Http.Interfaces;
using Innkeep.Api.Endpoints.Server;
using Innkeep.Api.Internal.Interfaces.Server.Checkin;
using Innkeep.Api.Internal.Repositories.Server.Core;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Api.Internal.Repositories.Server.Checkin;

public class CheckinRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ICheckinRepository
{
	protected override int Timeout => 1000;

	public async Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string secret)
	{
		var baseUri = await GetAddress();
		var uri = ServerUrlBuilder.Endpoints.Address(baseUri).Checkin.Entry.Parameters.Identifier(Identifier).Build();

		var data = new CheckinRequest
		{
			Secret = secret,
		};

		return await Post<CheckinRequest, PretixCheckinResponse>(uri, data);
	}
}