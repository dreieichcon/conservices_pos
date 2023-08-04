namespace Innkeep.Core.DomainModels.Authentication;

public class AuthenticationInfo
{
    public AuthenticationInfo(string pretixToken)
    {
        PretixToken = pretixToken;
    }

    public string PretixToken { get; set; }
}