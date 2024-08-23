using Innkeep.Api.Auth;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Models;
using Innkeep.Db.Server.Models.Server;
using Innkeep.Services.Interfaces;

namespace Innkeep.Services.Server.Authentication;

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

	public async Task<IEnumerable<FiskalyTseConfig>> GetAll() => await _tseConfigRepository.GetAllAsync();

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

	public async Task<bool> Import(FiskalyTseConfig? imported)
	{
		if (imported is null)
			return false;

		var existing = await _tseConfigRepository.GetAsync(imported.Id);

		if (existing is null)
		{
			var toSave = new FiskalyTseConfig()
			{
				OperationType = Operation.Created,
				TseAdminPassword = imported.TseAdminPassword,
				TseId = imported.TseId,
				TsePuk = imported.TsePuk,
			};

			var result = await _tseConfigRepository.CrudAsync(toSave);
			return result.Success;
		}
		
		
		return true;
	}
}