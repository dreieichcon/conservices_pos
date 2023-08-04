using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Server.Data.Interfaces;

public interface IAuthenticationRepository
{
	public AuthenticationInfo Get();

	public bool Update(AuthenticationInfo info);
}