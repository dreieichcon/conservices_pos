using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Innkeep.Api.Json;

public class PretixDecimalJsonConverter : JsonConverter<decimal>
{
	public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var input = reader.GetString()!;
		return ParseInput(input);
	}

	public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(ParseOutput(value));
	}

	public static decimal ParseInput(string? input)
	{
		if (input is null) return 0m;
		
		if (input.Contains(','))
			input = input.Replace(",", ".");
		
		return decimal.TryParse(input, CultureInfo.InvariantCulture, out var result) ? result : 0m;
	}

	public static string ParseOutput(decimal input)
	{
		return input.ToString("0.00", CultureInfo.InvariantCulture);
	}
}