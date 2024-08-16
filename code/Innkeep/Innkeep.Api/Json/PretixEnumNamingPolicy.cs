using System.Text.Json;

namespace Innkeep.Api.Json;

public class PretixEnumNamingPolicy : JsonNamingPolicy
{
	public override string ConvertName(string name)
	{
		return name.ToLowerInvariant();
	}
}