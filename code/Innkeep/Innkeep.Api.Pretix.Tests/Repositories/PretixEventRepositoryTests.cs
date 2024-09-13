using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Tests.Data;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixEventRepositoryTests
{
	private readonly ITestAuth _testAuth = new TestAuth();
	private PretixEventRepository _eventRepository = null!;

	[TestInitialize]
	public void Initialize()
	{
		var authenticationService = new PretixAuthenticationServiceMock();
		_eventRepository = new PretixEventRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Events_ListIncludesTestEvent()
	{
		var result = await _eventRepository.GetEvents(_testAuth.PretixTestOrganizerSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.FirstOrDefault(x => x.Slug.Equals(_testAuth.PretixTestEventSlug));

		Assert.IsNotNull(testResult);
	}
}