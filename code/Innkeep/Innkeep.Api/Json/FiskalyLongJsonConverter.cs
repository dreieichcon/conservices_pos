using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Json;

public class FiskalyLongJsonConverter : JsonConverter<long>
{
	public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			var input = reader.GetString()!;
			return ParseInput(input);
		}
		catch
		{
			return reader.GetInt64();
		}
	}

	public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	public static long ParseInput(string? input)
	{
		if (string.IsNullOrEmpty(input)) return 0L;

		return long.TryParse(input, CultureInfo.InvariantCulture, out var result) ? result : 0L;
	}

	public static string ParseOutput(long input) => input.ToString(CultureInfo.InvariantCulture);
}