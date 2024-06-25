﻿using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Repositories.Auth;
using Innkeep.Api.Fiskaly.Tests.Data;
using Innkeep.Api.Fiskaly.Tests.Mock;

namespace Innkeep.Api.Fiskaly.Tests.Repositories;

[TestClass]
public class FiskalyAuthRepositoryTests
{
	private FiskalyAuthRepository _authRepository = null!;

	private IFiskalyAuthenticationService _authenticationService = null!;

	[TestInitialize]
	public void Initialize()
	{
		_authRepository = new FiskalyAuthRepository();
		_authenticationService = new FiskalyAuthenticationServiceMock(_authRepository);
	}

	[TestMethod]
	public async Task Authenticate_ApiTestKeys_ResponseIsValidToken()
	{
		var testResult = await _authRepository.Authenticate(_authenticationService.AuthenticationInfo);
		
		Assert.IsNotNull(testResult);
	}
}