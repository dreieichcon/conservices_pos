using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Client.Interfaces.Checkin;
using Innkeep.Services.Interfaces;

namespace Innkeep.Services.Client.Checkin;

public class CheckinService(IDbService<ClientConfig> clientConfigService)
	: AbstractServerRepository(clientConfigService), ICheckinService
{
	public LinkedList<PretixCheckinResponse> LastCheckins { get; set; } = [];

	public async Task<PretixCheckinResponse?> CheckIn(string secret)
	{
		var baseUri = await GetAddress();
		var uri = new ServerEndpointBuilder(baseUri).WithCheckin().Entry().WithIdentifier(Identifier).Build();

		var data = new CheckinRequest()
		{
			Secret = secret,
		};

		var serialized = Serialize(data);

		var result = await Post(uri, serialized);

		var deserialized = DeserializeOrNull<PretixCheckinResponse>(result);

		if (deserialized == null)
			return deserialized;

		if (LastCheckins.Count > 3)
			LastCheckins.RemoveLast();

		LastCheckins.AddFirst(deserialized);

		return deserialized;
	}
}