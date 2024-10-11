using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Server.Models.Config;

namespace Innkeep.Api.Fiskaly.Tests.Mock;

public class FiskalyAuthenticationServiceMock : IFiskalyAuthenticationService
{
	private readonly IFiskalyAuthenticationRepository _authenticationRepository;

	public FiskalyAuthenticationServiceMock(IFiskalyAuthenticationRepository authenticationRepository)
	{
		var key = Environment.GetEnvironmentVariable("FISKALY_API_KEY") ?? string.Empty;
		var secret = Environment.GetEnvironmentVariable("FISKALY_API_SECRET") ?? string.Empty;

		AuthenticationInfo = new AuthenticationInfo
		{
			Key = key,
			Secret = secret,
		};

		CurrentConfig = new FiskalyTseConfig
		{
			Id = Guid.NewGuid()
					.ToString(),
			TseId = string.Empty,
		};

		_authenticationRepository = authenticationRepository;
	}

	public AuthenticationInfo AuthenticationInfo { get; set; }

	public FiskalyTseConfig CurrentConfig { get; set; }

	public async Task GetOrUpdateToken()
	{
		if (string.IsNullOrEmpty(AuthenticationInfo.Token) ||
			AuthenticationInfo.TokenValidUntil > DateTime.UtcNow - TimeSpan.FromMinutes(5))
		{
			var result = await _authenticationRepository.Authenticate(AuthenticationInfo);
			AuthenticationInfo.Token = result.Object?.Token ?? string.Empty;
			AuthenticationInfo.TokenValidUntil = result.Object?.TokenValidUntil ?? DateTime.UtcNow;
		}
	}

	public Task CreateTseConfig(FiskalyTss tss) => throw new NotImplementedException();

	public Task CreateTseConfig(string tseId) => throw new NotImplementedException();

	public Task<bool> SaveTseConfig() => throw new NotImplementedException();

	public Task<IEnumerable<FiskalyTseConfig>> GetAll() => throw new NotImplementedException();

	public Task<bool> Import(FiskalyTseConfig? imported) => throw new NotImplementedException();
}