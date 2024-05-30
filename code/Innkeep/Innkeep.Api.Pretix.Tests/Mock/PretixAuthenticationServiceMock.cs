using Innkeep.Api.Auth;
using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Pretix.Tests.Mock;

public class PretixAuthenticationServiceMock : IPretixAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; } = new(new Data.TestAuth().PretixTestToken);
}