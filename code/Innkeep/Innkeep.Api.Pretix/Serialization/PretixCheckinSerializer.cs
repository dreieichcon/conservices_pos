using System.Text.Json;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Data.Pretix.Serialization;

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