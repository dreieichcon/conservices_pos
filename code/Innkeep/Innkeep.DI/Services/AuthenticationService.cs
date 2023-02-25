using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Repositories;

namespace Innkeep.DI.Services;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationInfo AuthenticationInfo { get; set; }

    private readonly IAuthenticationRepository _authenticationRepository;

    public AuthenticationService(IAuthenticationRepository authenticationRepository)
    {
        _authenticationRepository = authenticationRepository;
        ReadAuthentication();
    }
    
    public void ReadAuthentication()
    {
        AuthenticationInfo = _authenticationRepository.Read();
    }

    public void SaveAuthentication()
    {
        throw new NotImplementedException();
    }
}