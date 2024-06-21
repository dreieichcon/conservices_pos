using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Authentication;

public class FiskalyAuthenticationService : IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; } = new(string.Empty);
	
	private readonly IDbService<FiskalyConfig> _configService;
	private readonly IFiskalyAuthRepository _authRepository;

	public FiskalyAuthenticationService(IDbService<FiskalyConfig> configService, IFiskalyAuthRepository authRepository)
	{
		_configService = configService;
		_authRepository = authRepository;
		_configService.ItemsUpdated += (_, _) => UpdateAuthentication();
	}

	private void UpdateAuthentication()
	{
		AuthenticationInfo.Key = _configService.CurrentItem.ApiKey;
		AuthenticationInfo.Secret = _configService.CurrentItem.ApiSecret;
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
}