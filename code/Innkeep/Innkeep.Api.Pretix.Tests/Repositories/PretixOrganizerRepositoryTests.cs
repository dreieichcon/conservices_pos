using Innkeep.Api.Pretix.Repositories;
using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Tests.Data;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixOrganizerRepositoryTests
{
	private PretixOrganizerRepository _organizerRepository = null!;
	
	private readonly ITestAuth _testAuth = new TestAuth();
	
	[TestInitialize]
	public void Initialize()
	{
		var authenticationService = new PretixAuthenticationServiceMock();
		_organizerRepository = new PretixOrganizerRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Organizers_ListIncludesTestOrganizer()
	{
		var result = await _organizerRepository.GetOrganizers();

		var testResult = result.Object?.FirstOrDefault(x => x.Slug.Equals(_testAuth.PretixTestOrganizerSlug));
		
		Assert.IsNotNull(testResult);
	}
}