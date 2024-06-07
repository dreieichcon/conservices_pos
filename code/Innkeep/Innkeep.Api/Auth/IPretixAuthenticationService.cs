using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Auth;

public interface IPretixAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; }

	public void Load();

}