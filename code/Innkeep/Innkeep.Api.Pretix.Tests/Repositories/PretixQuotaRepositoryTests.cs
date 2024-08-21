using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Quota;
using Innkeep.Api.Pretix.Tests.Data;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixQuotaRepositoryTests
{
	private IPretixQuotaRepository _quotaRepository = null!;
	
	private readonly ITestAuth _testAuth = new TestAuth();

	[TestInitialize]
	public void Initialize()
	{
		var authenticationService = new PretixAuthenticationServiceMock();
		_quotaRepository = new PretixQuotaRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Quotas_ReturnsItems()
	{
		var result = await _quotaRepository.GetAll(_testAuth.PretixTestOrganizerSlug, _testAuth.PretixTestEventSlug);
		Assert.IsTrue(result.Count > 0);
	}
}