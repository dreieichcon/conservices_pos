using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Data.Interfaces;

namespace Innkeep.Server.Services.Services.Db;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationInfo AuthenticationInfo { get; set; }

    private readonly IAuthenticationRepository _authenticationRepository;

    public AuthenticationService(IAuthenticationRepository authenticationRepository)
    {
        _authenticationRepository = authenticationRepository;
        ReadAuthentication();
    }

    public bool AuthenticationSuccessful { get; set; }

    public void ReadAuthentication()
    {
        AuthenticationInfo = _authenticationRepository.Get();
    }

    public void SaveAuthentication()
    {
        _authenticationRepository.Update(AuthenticationInfo);
    }
}