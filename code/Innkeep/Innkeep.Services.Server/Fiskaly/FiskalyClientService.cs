using Innkeep.Api.Enum.Fiskaly.Client;
using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Innkeep.Services.Server.Interfaces.Fiskaly;

namespace Innkeep.Services.Server.Fiskaly;

public class FiskalyClientService(IDbService<FiskalyConfig> configService, 
								IFiskalyClientRepository clientRepository,
								IHardwareService hardwareService)
: IFiskalyClientService
{
	private FiskalyClient? _currentClient;

	public event EventHandler? ItemsUpdated;

	public FiskalyClient? CurrentClient
	{
		get => _currentClient;
		set
		{
			_currentClient = value;
			configService.CurrentItem!.ClientId = _currentClient?.Id ?? string.Empty;
		}
	}

	public IEnumerable<FiskalyClient> Clients { get; set; } = new List<FiskalyClient>();

	public async Task Load()
	{
		await configService.Load();

		if (IsTssNull())
			return;

		Clients = await clientRepository.GetAll(configService.CurrentItem!.TseId);

		CurrentClient = Clients.FirstOrDefault(x => x.Id == configService.CurrentItem.ClientId);
		
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> CreateNew()
	{
		if (IsTssNull()) 
			return false;

		var clientResult = await clientRepository.CreateClient(
			configService.CurrentItem!.TseId,
			Guid.NewGuid().ToString(),
			GenerateSerialNumber()
		);

		CurrentClient = clientResult;
		
		ItemsUpdated?.Invoke(this, EventArgs.Empty);

		if (clientResult != null)
			await Save();
		
		return clientResult != null;
	}

	public async Task<bool> Deactivate()
	{
		if (IsTssNull() || CurrentClient is null)
			return false;

		var result = await clientRepository.UpdateClient(
			configService.CurrentItem!.TseId,
			CurrentClient.Id,
			ClientState.Deregistered
		);

		CurrentClient = result;

		return CurrentClient?.State == ClientState.Deregistered;
	}

	public async Task<bool> Activate()
	{
		if (IsTssNull() || CurrentClient is null)
			return false;

		var result = await clientRepository.UpdateClient(
			configService.CurrentItem!.TseId,
			CurrentClient.Id,
			ClientState.Registered
		);

		CurrentClient = result;

		return CurrentClient?.State == ClientState.Registered;
	}

	public async Task<bool> Save()
	{
		var result = await configService.Save();
		await Load();

		return result;
	}

	private bool IsTssNull() => string.IsNullOrEmpty(configService.CurrentItem?.TseId);

	private string GenerateSerialNumber()
	{
		return hardwareService.ClientIdentifier != "NO IDENTIFIER" 
			? hardwareService.ClientIdentifier 
			: Guid.NewGuid().ToString().Replace("-", "");
	}
	
}