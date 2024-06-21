using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Auth;

public interface IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; }

	public Task GetOrUpdateToken();
}