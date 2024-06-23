using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Authentication;

public class FiskalyAuthenticationService : IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; } = new(string.Empty);
	
	public FiskalyTseConfig CurrentConfig { get; set; }
	
	private readonly IDbService<FiskalyConfig> _configService;
	private readonly IDbRepository<FiskalyTseConfig> _tseConfigRepository;
	private readonly IFiskalyAuthRepository _authRepository;

	public FiskalyAuthenticationService(
		IDbService<FiskalyConfig> configService, 
		IDbRepository<FiskalyTseConfig> tseConfigRepository,
		IFiskalyAuthRepository authRepository)
	{
		_tseConfigRepository = tseConfigRepository;
		_configService = configService;
		_authRepository = authRepository;
		_configService.ItemsUpdated += (_, _) => UpdateAuthentication();
		_configService.ItemsUpdated += async (_, _) => await RetrieveTseConfig();
	}

	private async Task RetrieveTseConfig()
	{
		var tseId = _configService.CurrentItem.TseId;
		var itemFromDb = await _tseConfigRepository.GetCustomAsync(x => x.TseId == tseId);

		if (itemFromDb is null)
		{
			await CreateTseConfig(tseId);
			return;
		}

		CurrentConfig = itemFromDb;
		CurrentConfig.OperationType = Operation.Updated;
	}

	public async Task CreateTseConfig(FiskalyTss tss)
	{
		var item = new FiskalyTseConfig()
		{
			OperationType = Operation.Created,
			TseId = tss.Id,
			TsePuk = tss.AdminPuk,
		};

		await _tseConfigRepository.CrudAsync(item);
		await RetrieveTseConfig();
	}

	public async Task CreateTseConfig(string tseId)
	{
		var item = new FiskalyTseConfig()
		{
			OperationType = Operation.Created,
			TseId = tseId,
		};

		await _tseConfigRepository.CrudAsync(item);
		await RetrieveTseConfig();
	}

	public async Task<bool> SaveTseConfig()
	{
		var result = await _tseConfigRepository.CrudAsync(CurrentConfig);
		await RetrieveTseConfig();

		return result.Success;
	}

	private void UpdateAuthentication()
	{
		AuthenticationInfo.Key = _configService.CurrentItem.ApiKey;
		AuthenticationInfo.Secret = _configService.CurrentItem.ApiSecret;
	}

	public async Task GetOrUpdateToken()
	{
		UpdateAuthentication();
		
		if (string.IsNullOrEmpty(AuthenticationInfo.Token) ||
			AuthenticationInfo.TokenValidUntil > DateTime.UtcNow - TimeSpan.FromMinutes(5))
		{
			var result = await _authRepository.Authenticate(AuthenticationInfo);
			AuthenticationInfo.Token = result?.Token ?? string.Empty;
			AuthenticationInfo.TokenValidUntil = result?.TokenValidUntil ?? DateTime.UtcNow;
		}
	}
}