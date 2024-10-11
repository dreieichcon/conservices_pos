using Innkeep.Core.Env;

namespace Innkeep.Api.Pretix.Tests.Repositories;

public abstract class AbstractPretixRepositoryTest
{
	protected string PretixEventSlug = string.Empty;
	protected string PretixOrganizerSlug = string.Empty;
	protected string PretixTestItemName = string.Empty;

	public virtual void Initialize()
	{
		var result = Env.Load("../../../../Env/tests.env");

		PretixOrganizerSlug = Environment.GetEnvironmentVariable("PRETIX_ORGANIZER_SLUG") ?? string.Empty;
		PretixEventSlug = Environment.GetEnvironmentVariable("PRETIX_EVENT_SLUG") ?? string.Empty;
		PretixTestItemName = Environment.GetEnvironmentVariable("PRETIX_TEST_ITEM_NAME") ?? string.Empty;
	}
}