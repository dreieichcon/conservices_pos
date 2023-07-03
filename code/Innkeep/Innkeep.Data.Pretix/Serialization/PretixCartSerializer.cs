using System.Collections;
using System.Text.Json;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Data.Pretix.Serialization;

public static class PretixCartSerializer
{
	public static string SerializeTransaction(IEnumerable<PretixCartItem<PretixSalesItem>> salesItems, bool isTest = false)
	{
		return JsonSerializer.Serialize(CreateOrder(salesItems, isTest));
	}

	private static PretixOrder CreateOrder(IEnumerable<PretixCartItem<PretixSalesItem>> salesItems, bool isTest = false)
	{
		var order = new PretixOrder()
		{
			Status = "p",
			Locale = "de",
			PaymentProvider = "manual",
			SalesChannel = "pretixpos",
			Mail = "",
			Customer = null,
			InvoiceAddress = null,
			Positions = CreatePositions(salesItems),
			Simulate = isTest,
		};

		return order;
	}

	private static IList<PretixOrderPosition> CreatePositions(IEnumerable<PretixCartItem<PretixSalesItem>> salesItems)
	{
		var positions = new List<PretixOrderPosition>();
		var position = 1;

		foreach (var item in salesItems)
		{
			for (var i = 0; i < item.Count; i++)
			{
				positions.Add(new PretixOrderPosition()
				{
					PositionId = position,
					Item = item.Item.Id,
					Price = item.Item.DefaultPrice,
					Variation = null,
					AttendeeName = "internal_pos"
				});

				position++;
			}
			
		}

		return positions;
	}
}