using System.Text.Json;

namespace Innkeep.Api.Json;

public static class SerializerOptions
{
	public static JsonSerializerOptions GetServerOptions()
	{
		return new JsonSerializerOptions()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
			},
			WriteIndented = true,
		};
	}
}