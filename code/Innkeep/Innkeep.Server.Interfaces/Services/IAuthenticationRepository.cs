using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Server.Interfaces.Services;

public interface IAuthenticationRepository
{
	public AuthenticationInfo Get();

	public bool Update(AuthenticationInfo info);
}