using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Innkeep.Server.Api.Converters;

public class DecimalJsonConverter : JsonConverter<Decimal>
{
	public override Decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		try
		{
			var value = reader.GetString().Replace(".", ",");
			return decimal.Parse(value!);
		}
		catch
		{
			var decimalValue = reader.GetDecimal();
			return decimalValue;
		}
		
		
	}

	public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
	}
}