using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;

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

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Amount Given: {AmountGiven}");

		if (!TransactionItems.Any()) return sb.ToString();
		
		foreach (var item in TransactionItems)
		{
			sb.AppendLine(item.Item.ToString());
		}

		return sb.ToString();
	}
}