using Innkeep.Api.Pretix.Repositories.Sales;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixSalesItemRepositoryTests : AbstractPretixRepositoryTest
{
	private PretixSalesItemRepository _itemRepository = null!;

	[TestInitialize]
	public override void Initialize()
	{
		base.Initialize();

		var authenticationService = new PretixAuthenticationServiceMock();
		_itemRepository = new PretixSalesItemRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_SalesItems_ListIncludesTestItem()
	{
		var result = await _itemRepository.GetItems(PretixOrganizerSlug, PretixEventSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.FirstOrDefault(x => x.Name.German.Equals(PretixTestItemName));

		Assert.IsNotNull(testResult);
	}

	[TestMethod]
	public async Task Get_SalesItem_PriceParsedCorrectly()
	{
		var result = await _itemRepository.GetItems(PretixOrganizerSlug, PretixEventSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.First(x => x.Name.German.Equals(PretixTestItemName));

		Assert.AreEqual(10m, testResult.DefaultPrice);
	}
}