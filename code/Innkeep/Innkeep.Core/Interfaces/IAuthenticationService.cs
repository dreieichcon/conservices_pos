using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Core.Interfaces;

public interface IAuthenticationService
{
    public AuthenticationInfo AuthenticationInfo { get; set; }

    public bool AuthenticationSuccessful { get; set; }

    public void ReadAuthentication();

    public void SaveAuthentication();
}