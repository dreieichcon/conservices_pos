using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Server.Data.Interfaces.ApplicationSettings;

public interface IAuthenticationRepository
{
	public AuthenticationInfo Get();

	public bool Update(AuthenticationInfo info);
}