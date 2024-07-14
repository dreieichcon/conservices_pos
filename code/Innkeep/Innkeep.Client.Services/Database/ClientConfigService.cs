using Innkeep.Db.Client.Models;
using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Services.Interfaces;

namespace Innkeep.Client.Services.Database;

public class ClientConfigService(IDbRepository<ClientConfig> clientConfigRepository) : IDbService<ClientConfig>
{
	public event EventHandler? ItemsUpdated;

	public ClientConfig CurrentItem { get; set; }

	public IEnumerable<ClientConfig> Items { get; set; }

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
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		return (await clientConfigRepository.CrudAsync(CurrentItem)).Success;
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