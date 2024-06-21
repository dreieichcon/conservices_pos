namespace Innkeep.Core.DomainModels.Authentication;

public class AuthenticationInfo
{
    public AuthenticationInfo()
    {
        
    }
    
    public AuthenticationInfo(string token)
    {
        Token = token;
    }

    public string Token { get; set; }

    public string Key { get; set; } = string.Empty;
    
    public string Secret { get; set; } = string.Empty;
    
    public DateTime? TokenValidUntil { get; set; }
}