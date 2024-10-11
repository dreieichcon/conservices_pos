using Innkeep.Api.Pretix.Repositories.General;
using Innkeep.Api.Pretix.Tests.Mock;

namespace Innkeep.Api.Pretix.Tests.Repositories;

[TestClass]
public class PretixEventRepositoryTests : AbstractPretixRepositoryTest
{
	private PretixEventRepository _eventRepository = null!;

	[TestInitialize]
	public override void Initialize()
	{
		base.Initialize();

		var authenticationService = new PretixAuthenticationServiceMock();
		_eventRepository = new PretixEventRepository(authenticationService);
	}

	[TestMethod]
	public async Task Get_Events_ListIncludesTestEvent()
	{
		var result = await _eventRepository.GetEvents(PretixOrganizerSlug);

		Assert.IsTrue(result.Object?.Any());

		var testResult = result.Object!.FirstOrDefault(x => x.Slug.Equals(PretixEventSlug));

		Assert.IsNotNull(testResult);
	}

	[TestMethod]
	public async Task Get_Event_ReturnsPretixEvent()
	{
		var result = await _eventRepository.GetEvent(PretixOrganizerSlug, PretixEventSlug);

		Assert.IsTrue(result.IsSuccess);
		Assert.AreEqual(PretixEventSlug, result.Object?.Slug);
	}

	[TestMethod]
	public async Task Get_EventSettings_ListIncludesTestEvent()
	{
		var result = await _eventRepository.GetEventSettings(PretixOrganizerSlug, PretixEventSlug);

		Assert.IsTrue(result.IsSuccess);
	}
}