using System.Text.Json;
using Innkeep.Api.Pretix.Models.Objects;

namespace Innkeep.Api.Pretix.Serialization;

public static class PretixCheckinSerializer
{
	public static string SerializeCheckin(PretixOrderResponse orderResponse)
	{
		return JsonSerializer.Serialize(GenerateCheckin(orderResponse));
	}

	public static string SerializeCheckin(PretixCheckin pretixCheckin)
	{
		return JsonSerializer.Serialize(pretixCheckin);
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