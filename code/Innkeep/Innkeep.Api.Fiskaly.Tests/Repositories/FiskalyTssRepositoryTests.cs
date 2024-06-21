using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Repositories.Auth;
using Innkeep.Api.Fiskaly.Repositories.Tss;
using Innkeep.Api.Fiskaly.Tests.Mock;

namespace Innkeep.Api.Fiskaly.Tests.Repositories;

[TestClass]
public class FiskalyTssRepositoryTests
{
	private FiskalyAuthRepository _authRepository = null!;

	private IFiskalyAuthenticationService _authenticationService;

	private FiskalyTssRepository _tssRepository;

	[TestInitialize]
	public void Initialize()
	{
		_authRepository = new FiskalyAuthRepository();
		_authenticationService = new FiskalyAuthenticationServiceMock(_authRepository);
		_tssRepository = new FiskalyTssRepository(_authenticationService);
	}

	[TestMethod]
	public async Task Get_MultipleTss_FetchedCorrectly()
	{
		var result = await _tssRepository.GetAll();
		Assert.IsTrue(result.Any());
	}
	
}