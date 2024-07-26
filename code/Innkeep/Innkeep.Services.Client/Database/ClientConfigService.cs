using Innkeep.Db.Client.Models;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Services.Interfaces;

namespace Innkeep.Services.Client.Database;

public class ClientConfigService : IDbService<ClientConfig>
{
	private readonly IDbRepository<ClientConfig> _clientConfigRepository;
	private readonly IHardwareService _hardwareService;

	public ClientConfigService(IDbRepository<ClientConfig> clientConfigRepository, IHardwareService hardwareService)
	{
		_clientConfigRepository = clientConfigRepository;
		_hardwareService = hardwareService;
	}

	public event EventHandler? ItemsUpdated;

	public ClientConfig? CurrentItem { get; set; }

	public IEnumerable<ClientConfig> Items { get; set; }

	public async Task Load()
	{
		var dbItem = (await _clientConfigRepository.GetAllAsync()).FirstOrDefault();

		if (dbItem is null)
		{
			await Create();
			return;
		}

		dbItem.OperationType = Operation.Updated;
		CurrentItem = dbItem;
		CurrentItem.HardwareIdentifier = _hardwareService.ClientIdentifier;
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		return (await _clientConfigRepository.CrudAsync(CurrentItem)).Success;
	}

	private async Task Create()
	{
		CurrentItem = new ClientConfig()
		{
			OperationType = Operation.Created,
		};

		await Save();
		await Load();
	}
}