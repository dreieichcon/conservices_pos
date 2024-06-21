using Innkeep.Api.Fiskaly.Repositories.Auth;
using Innkeep.Api.Fiskaly.Tests.Data;
using Innkeep.Api.Fiskaly.Tests.Mock;

namespace Innkeep.Api.Fiskaly.Tests.Repositories;

[TestClass]
public class FiskalyAuthRepositoryTests
{
	private FiskalyAuthRepository _authRepository = null!;

	private readonly ITestAuth _testAuth = new TestAuth();

	[TestInitialize]
	public void Initialize()
	{
		var authService = new FiskalyAuthenticationServiceMock();
		_authRepository = new FiskalyAuthRepository(authService);
	}

	[TestMethod]
	public async Task Authenticate_ApiTestKeys_ResponseIsValidToken()
	{
		var testResult = await _authRepository.Authenticate();
		
		Assert.IsNotNull(testResult);
	}
}