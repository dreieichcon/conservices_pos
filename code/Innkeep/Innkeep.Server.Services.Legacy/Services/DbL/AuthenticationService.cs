using Innkeep.Api.Pretix.Legacy.Interfaces;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;

namespace Innkeep.Server.Services.Legacy.Services.Db;

public class AuthenticationService : IAuthenticationService
{
    public required AuthenticationInfo AuthenticationInfo { get; set; }

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