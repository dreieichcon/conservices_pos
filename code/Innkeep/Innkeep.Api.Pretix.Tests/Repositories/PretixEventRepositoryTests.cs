using Innkeep.Api.Pretix.Repositories;
using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Tests.Data;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixEventRepositoryTests
{
	private PretixEventRepository _eventRepository = null!;

	private readonly ITestAuth _testAuth = new TestAuth();

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

		var testResult = result.FirstOrDefault(x => x.Slug.Equals(_testAuth.PretixTestEventSlug));
		
		Assert.IsNotNull(testResult);
	}
}