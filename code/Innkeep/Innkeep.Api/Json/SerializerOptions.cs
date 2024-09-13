using System.Text.Json;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Json;

public static class SerializerOptions
{
	public static JsonSerializerOptions GetServerOptions()
		=> new()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
			},
			WriteIndented = true,
		};

	public static JsonSerializerOptions GetOptionsForPretix()
		=> new()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
				new JsonStringEnumConverter(new PretixEnumNamingPolicy()),
			},
		};

	public static JsonSerializerOptions GetOptionsForFiskaly() => new()
	{
		Converters =
		{
			new PretixDecimalJsonConverter(),
			new FiskalyDateTimeJsonConverter(),
			new FiskalyLongJsonConverter(),
			new JsonStringEnumConverter(new FiskalyEnumNamingPolicy()),
		},
	};
}