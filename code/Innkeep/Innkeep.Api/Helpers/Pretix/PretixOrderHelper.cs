using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;

namespace Innkeep.Api.Helpers.Pretix;

public static class PretixOrderHelper
{
	public static PretixOrder CreateOrder(
		IEnumerable<DtoSalesItem> cart,
		bool isTestMode
	)
	{
		return new PretixOrder()
		{
			Mail = "",
			Locale = "de",
			TestMode = isTestMode,
			Positions = CreatePositions(cart),
		};
	}

	private static List<PretixOrderPosition> CreatePositions(IEnumerable<DtoSalesItem> cart)
	{
		var positions = new List<PretixOrderPosition>();
		var currentPosition = 1;
		
		foreach (var item in cart)
		{
			for (var i = 0; i < item.CartCount; i++)
			{
				positions.Add(new PretixOrderPosition()
				{
					PositionId = currentPosition,
					AttendeeName = "internal_pos",
					Item = item.Id,
					Price = item.Price,
					Variation = null,
				});

				currentPosition++;
			}
		}

		return positions;
	}
}