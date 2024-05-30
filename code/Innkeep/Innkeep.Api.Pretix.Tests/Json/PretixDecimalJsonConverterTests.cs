using Innkeep.Api.Json;

namespace Innkeep.Api.Pretix.Tests.Json;

[TestClass]
public class PretixDecimalJsonConverterTests
{
	private PretixDecimalJsonConverter _converter = new();

	[TestMethod]
	public void Read_DecimalWithPoint_IsParsedCorrectly()
	{
		var result = PretixDecimalJsonConverter.ParseInput("10.00");
		Assert.AreEqual(10m, result);
	}

	[TestMethod]
	public void Read_DecimalWithComma_IsParsedCorrectly()
	{
		var result = PretixDecimalJsonConverter.ParseInput("10,00");
		Assert.AreEqual(10m, result);
	}

	[TestMethod]
	public void Write_Decimal_IsOutputCorrect()
	{
		var result = PretixDecimalJsonConverter.ParseOutput(10m);
		Assert.AreEqual("10.00", result);
	}
}