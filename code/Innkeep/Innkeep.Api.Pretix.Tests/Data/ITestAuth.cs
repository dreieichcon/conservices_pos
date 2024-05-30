namespace Innkeep.Api.Pretix.Tests.Data;

public interface ITestAuth
{
	public string PretixTestToken { get; }
	
	public string PretixTestOrganizerSlug { get; }
}