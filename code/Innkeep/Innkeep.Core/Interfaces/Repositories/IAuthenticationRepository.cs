using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Core.Interfaces.Repositories;

public interface IAuthenticationRepository
{
    public AuthenticationInfo? Read();

    public void Write(AuthenticationInfo info);
}