using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Fiskaly.Tests.Data;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Server.Models;

namespace Innkeep.Api.Fiskaly.Tests.Mock;

public class FiskalyAuthenticationServiceMock : IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; }

	public FiskalyTseConfig CurrentConfig { get; set; }

	private readonly IFiskalyAuthRepository _authRepository;

	public FiskalyAuthenticationServiceMock(IFiskalyAuthRepository authRepository)
	{
		var testAuth = new TestAuth();

		AuthenticationInfo = new AuthenticationInfo()
		{
			Key = testAuth.FiskalyApiKey,
			Secret = testAuth.FiskalyApiSecret,
		};

		CurrentConfig = new FiskalyTseConfig()
		{
			Id = Guid.NewGuid().ToString(),
			TseId = string.Empty
		};

		_authRepository = authRepository;
	}

	public async Task GetOrUpdateToken()
	{
		if (string.IsNullOrEmpty(AuthenticationInfo.Token) ||
			AuthenticationInfo.TokenValidUntil > DateTime.UtcNow - TimeSpan.FromMinutes(5))
		{
			var result = await _authRepository.Authenticate(AuthenticationInfo);
			AuthenticationInfo.Token = result?.Token ?? string.Empty;
			AuthenticationInfo.TokenValidUntil = result?.TokenValidUntil ?? DateTime.UtcNow;
		}
	}

	public Task CreateTseConfig(FiskalyTss tss) => throw new NotImplementedException();

	public Task CreateTseConfig(string tseId) => throw new NotImplementedException();

	public Task SaveTseConfig() => throw new NotImplementedException();
}