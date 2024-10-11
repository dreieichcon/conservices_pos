using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixOrganizerRepositoryTests : AbstractPretixRepositoryTest
{
	private PretixOrganizerRepository _organizerRepository = null!;

	[TestInitialize]
	public override void Initialize()
	{
		base.Initialize();

		var authenticationService = new PretixAuthenticationServiceMock();
		_organizerRepository = new PretixOrganizerRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Organizers_ListIncludesTestOrganizer()
	{
		var result = await _organizerRepository.GetOrganizers();

		var testResult = result.Object?.FirstOrDefault(x => x.Slug.Equals(PretixOrganizerSlug));

		Assert.IsNotNull(testResult);
	}
}