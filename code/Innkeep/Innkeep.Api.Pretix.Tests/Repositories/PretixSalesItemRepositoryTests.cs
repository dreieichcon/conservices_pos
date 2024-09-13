using Innkeep.Api.Pretix.Repositories.Sales;
using Innkeep.Api.Pretix.Tests.Data;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixSalesItemRepositoryTests
{
	private readonly ITestAuth _testAuth = new TestAuth();
	private PretixSalesItemRepository _itemRepository = null!;

	[TestInitialize]
	public void Initialize()
	{
		var authenticationService = new PretixAuthenticationServiceMock();
		_itemRepository = new PretixSalesItemRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_SalesItems_ListIncludesTestItem()
	{
		var result = await _itemRepository.GetItems(_testAuth.PretixTestOrganizerSlug, _testAuth.PretixTestEventSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.FirstOrDefault(x => x.Name.German.Equals(_testAuth.PretixTestItemName));

		Assert.IsNotNull(testResult);
	}

	[TestMethod]
	public async Task Get_SalesItem_PriceParsedCorrectly()
	{
		var result = await _itemRepository.GetItems(_testAuth.PretixTestOrganizerSlug, _testAuth.PretixTestEventSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.First(x => x.Name.German.Equals(_testAuth.PretixTestItemName));

		Assert.AreEqual(10m, testResult.DefaultPrice);
	}
}