using Demolite.Db.Enum;
using Demolite.Db.Interfaces;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Interfaces.Hardware;

namespace Innkeep.Services.Client.Database;

public class ClientConfigService(IDbRepository<ClientConfig> clientConfigRepository, IHardwareService hardwareService)
	: IDbService<ClientConfig>
{
	public event EventHandler? ItemsUpdated;

	public ClientConfig? CurrentItem { get; set; }

	public IEnumerable<ClientConfig> Items { get; set; } = [];

	public async Task Load()
	{
		var dbItem = (await clientConfigRepository.GetAllAsync()).FirstOrDefault();

		if (dbItem is null)
		{
			await Create();
			return;
		}

		dbItem.OperationType = Operation.Updated;
		CurrentItem = dbItem;
		CurrentItem.HardwareIdentifier = hardwareService.ClientIdentifier;
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		return (await clientConfigRepository.CrudAsync(CurrentItem!)).Success;
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