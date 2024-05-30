using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Innkeep.Api.Pretix.Legacy.Models.Internal;
using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Json;

namespace Innkeep.Models.Transaction;

public class PretixTransaction
{
	public PretixTransaction()
	{
		
	}
	
	public PretixTransaction(IEnumerable<PretixCartItem<PretixSalesItem>> transactionItems, decimal amountGiven, DateTime transactionStart)
	{
		TransactionItems = transactionItems;
		AmountGiven = amountGiven;
		TransactionStart = transactionStart;
	}

	[JsonPropertyName("TransactionItems")]
	public IEnumerable<PretixCartItem<PretixSalesItem>> TransactionItems { get; set; } =
		new List<PretixCartItem<PretixSalesItem>>();

	[JsonPropertyName("AmountGiven")]
	public decimal AmountGiven { get; set; }

	public decimal Sum => TransactionItems.Sum(x => x.Price);

	public decimal Return => Sum - AmountGiven;
	
	public DateTime TransactionStart { get; set; }
	
	public Guid TransactionId { get; set; }

	public string SerializeTransaction()
	{
		return JsonSerializer.Serialize(this);
	}
	
	public static PretixTransaction DeserializeTransaction(string items)
	{
		return JsonSerializer.Deserialize<PretixTransaction>(items)!;
	}

	public static int GetLines(string items)
	{
		var raw = JsonSerializer.Deserialize<PretixTransaction>(items, new JsonSerializerOptions()
		{
			Converters = { new DecimalJsonConverter() }
		})!.ToString();
		
		var lines = raw.Split(Environment.NewLine).Length;

		return lines;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Amount Due: {Sum}");
		sb.AppendLine($"Amount Given: {AmountGiven}");
		sb.AppendLine($"Amount Returned: {Return}");

		if (!TransactionItems.Any()) return sb.ToString();
		
		foreach (var item in TransactionItems)
		{
			sb.AppendLine(item.Count+"x");
			sb.AppendLine(item.Item.ToString());
		}

		return sb.ToString();
	}
}