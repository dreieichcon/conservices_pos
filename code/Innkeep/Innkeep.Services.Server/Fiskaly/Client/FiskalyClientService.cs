using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Db.Server.Models.Config;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;
using Innkeep.Services.Server.Interfaces.Fiskaly.Client;

namespace Innkeep.Services.Server.Fiskaly.Client;

public partial class FiskalyClientService(
	IDbService<FiskalyConfig> configService,
	IFiskalyClientRepository clientRepository,
	IHardwareService hardwareService
) : IFiskalyClientService
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

		var result = await clientRepository.GetAll(configService.CurrentItem!.TseId);

		Clients = result.Object ?? [];

		CurrentClient = Clients.FirstOrDefault(x => x.Id == configService.CurrentItem.ClientId);

		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		var result = await configService.Save();
		await Load();

		return result;
	}

	private bool IsTssNull() => string.IsNullOrEmpty(configService.CurrentItem?.TseId);

	private string GenerateSerialNumber() => hardwareService.ClientIdentifier != "NO IDENTIFIER"
		? hardwareService.ClientIdentifier
		: Guid.NewGuid().ToString().Replace("-", "");
}