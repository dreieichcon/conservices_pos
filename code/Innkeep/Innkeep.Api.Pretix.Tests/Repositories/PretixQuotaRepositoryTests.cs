using Innkeep.Api.Pretix.Interfaces.Quota;
using Innkeep.Api.Pretix.Repositories.Quota;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixQuotaRepositoryTests : AbstractPretixRepositoryTest
{
	private IPretixQuotaRepository _quotaRepository = null!;

	[TestInitialize]
	public override void Initialize()
	{
		base.Initialize();

		var authenticationService = new PretixAuthenticationServiceMock();
		_quotaRepository = new PretixQuotaRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Quotas_ReturnsItems()
	{
		var result = await _quotaRepository.GetAll(PretixOrganizerSlug, PretixEventSlug);
		Assert.IsTrue(result.Object?.Any());
	}
}