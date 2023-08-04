using System.Text.Json;
using Innkeep.Api.Pretix.Models.Objects;

namespace Innkeep.Api.Pretix.Serialization;

public static class PretixCheckinSerializer
{
	public static string SerializeCheckin(PretixOrderResponse orderResponse)
	{
		return JsonSerializer.Serialize(GenerateCheckin(orderResponse));
	}

	private static PretixCheckin GenerateCheckin(PretixOrderResponse orderResponse)
	{
		return new PretixCheckin()
		{
			Secret = orderResponse.Secret,
			SourceType = "barcode",
			Type = "entry",
			Force = true,
		};
	}
}