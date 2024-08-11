using Innkeep.Api.Endpoints;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Core;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Server.Interfaces;
using Innkeep.Api.Server.Repositories.Core;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Innkeep.Api.Server.Repositories.Pos;

public class SalesItemRepository : AbstractServerRepository, ISalesItemRepository
{
	public SalesItemRepository(IDbService<ClientConfig> clientConfigService) : base(clientConfigService)
	{
	}

	public async Task<IList<DtoSalesItem>> GetSalesItems()
	{
		var baseUri = await GetAddress();
		var uri = new ServerEndpointBuilder(baseUri)
				.WithItems()
				.GetAll()
				.WithIdentifier(Identifier)
				.Build();

		var response = await Get(uri);
		

		var deserialized = TryDeserialize<DtoSalesItem[]>(response, []);

		return deserialized.ToList();
	}

	private static T TryDeserialize<T>(ApiResponse response, T defaultValue = default!)
	{
		try
		{
			return JsonSerializer.Deserialize<T>(response.Content, SerializerOptions.GetServerOptions()) ??
					defaultValue;
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Error while deserializing into {Type}:", typeof(T));

			return defaultValue;
		}
	}
}