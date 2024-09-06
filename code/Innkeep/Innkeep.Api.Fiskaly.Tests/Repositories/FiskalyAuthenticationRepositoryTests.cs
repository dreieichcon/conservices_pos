using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Repositories.Auth;
using Innkeep.Api.Fiskaly.Tests.Mock;

namespace Innkeep.Api.Fiskaly.Tests.Repositories;

[TestClass]
public class FiskalyAuthenticationRepositoryTests
{
	private FiskalyAuthenticationRepository _authenticationRepository = null!;

	private IFiskalyAuthenticationService _authenticationService = null!;

	[TestInitialize]
	public void Initialize()
	{
		_authenticationRepository = new FiskalyAuthenticationRepository();
		_authenticationService = new FiskalyAuthenticationServiceMock(_authenticationRepository);
	}

	[TestMethod]
	public async Task Authenticate_ApiTestKeys_ResponseIsValidToken()
	{
		var testResult = await _authenticationRepository.Authenticate(_authenticationService.AuthenticationInfo);

		Assert.IsNotNull(testResult);
	}
}