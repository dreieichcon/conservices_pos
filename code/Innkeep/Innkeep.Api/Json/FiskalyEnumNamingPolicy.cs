using System.Text.Json;

namespace Innkeep.Api.Json;

public class FiskalyEnumNamingPolicy : JsonNamingPolicy
{
	public override string ConvertName(string name)
	{
		return name.ToUpperInvariant();
	}
}