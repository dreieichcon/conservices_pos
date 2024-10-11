using Innkeep.Api.Auth;
using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Pretix.Tests.Mock;

public class PretixAuthenticationServiceMock : IPretixAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; } =
		new(Environment.GetEnvironmentVariable("PRETIX_API_TOKEN")!);

	public void Load() => throw new NotImplementedException();
}