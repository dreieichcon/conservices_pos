using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Tests.Data;
using Innkeep.Core.DomainModels.Authentication;

namespace Innkeep.Api.Fiskaly.Tests.Mock;

public class FiskalyAuthenticationServiceMock : IFiskalyAuthenticationService
{
	public FiskalyAuthenticationServiceMock()
	{
		var testAuth = new TestAuth();

		AuthenticationInfo = new AuthenticationInfo()
		{
			Key = testAuth.FiskalyApiKey,
			Secret = testAuth.FiskalyApiSecret,
		};
	}
	public AuthenticationInfo AuthenticationInfo { get; set; } 

	public Task GetOrUpdateToken() => throw new NotImplementedException();
}