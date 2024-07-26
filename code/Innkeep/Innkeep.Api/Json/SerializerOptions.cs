using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;

namespace Innkeep.Api.Json;

public static class SerializerOptions
{
	public static JsonSerializerOptions GetPretixOptions()
	{
		return new JsonSerializerOptions()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
			},
		};
	}
	
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

	public static JsonSerializerSettings GetServerOptionsNewtonsoft()
	{
		return new JsonSerializerSettings()
		{
			Formatting = Formatting.Indented,
			Culture = CultureInfo.InvariantCulture,
		};
	}
}