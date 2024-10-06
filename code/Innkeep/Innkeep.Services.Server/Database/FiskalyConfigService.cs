using Demolite.Db.Enum;
using Demolite.Db.Interfaces;
using Innkeep.Db.Server.Models.Config;
using Innkeep.Services.Interfaces;

namespace Innkeep.Services.Server.Database;

public class FiskalyConfigService(IDbRepository<FiskalyConfig> fiskalyConfigRepository) : IDbService<FiskalyConfig>
{
	public event EventHandler? ItemsUpdated;

	public FiskalyConfig CurrentItem { get; set; }

	public IEnumerable<FiskalyConfig> Items { get; set; } = new List<FiskalyConfig>();

	public async Task Load()
	{
		var items = await fiskalyConfigRepository.GetAllAsync();
		var item = items.FirstOrDefault();

		if (item is null)
		{
			await Create();
			return;
		}

		CurrentItem = item;
		CurrentItem.OperationType = Operation.Updated;

		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<bool> Save()
	{
		var result = await fiskalyConfigRepository.CrudAsync(CurrentItem);

		ItemsUpdated?.Invoke(this, EventArgs.Empty);
		return result.Success;
	}

	private async Task Create()
	{
		var item = new FiskalyConfig
		{
			OperationType = Operation.Created,
			ClientId = string.Empty,
			TseId = string.Empty,
		};

		await fiskalyConfigRepository.CrudAsync(item);
		await Load();
	}
}