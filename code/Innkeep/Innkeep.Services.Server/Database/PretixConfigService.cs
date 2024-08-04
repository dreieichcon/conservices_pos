using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Services.Server.Database;

public class PretixConfigService(IDbRepository<PretixConfig> pretixConfigRepository) : IDbService<PretixConfig>
{
	public event EventHandler? ItemsUpdated;

	public PretixConfig CurrentItem { get; set; }

	public IEnumerable<PretixConfig> Items { get; set; } = new List<PretixConfig>();

	public async Task Load()
	{
		var item = (await pretixConfigRepository.GetAllAsync()).FirstOrDefault();

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
		var result = await pretixConfigRepository.CrudAsync(CurrentItem);
		await Load();

		return result.Success;
	}

	private async Task Create()
	{
		CurrentItem = new PretixConfig()
		{
			OperationType = Operation.Created,
		};

		await pretixConfigRepository.CrudAsync(CurrentItem);
		await Load();
	}
}