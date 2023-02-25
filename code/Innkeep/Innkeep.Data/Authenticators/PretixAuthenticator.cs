using RestSharp;
using RestSharp.Authenticators;

namespace Innkeep.Data.Authenticators;

public static class PretixAuthenticator
{

    public static void AuthenticateRequest(RestRequest request, string token)
    {
        request.AddHeader("Authorization", $"Token {token}");
    }
}