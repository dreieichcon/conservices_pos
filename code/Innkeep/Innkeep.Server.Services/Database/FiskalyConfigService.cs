using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Database;

public class FiskalyConfigService(IDbRepository<FiskalyConfig> fiskalyConfigRepository) : IDbService<FiskalyConfig>
{
	public event EventHandler? ItemsUpdated;

	public FiskalyConfig CurrentItem { get; set; }

	public IEnumerable<FiskalyConfig> Items { get; set; } = new List<FiskalyConfig>();

	public async Task Load()
	{
		var item = (await fiskalyConfigRepository.GetAllAsync()).FirstOrDefault();
		
		if (item is null)
		{
			await Create();
			return;
		}
		
		CurrentItem = item;
		CurrentItem.OperationType = Operation.Updated;
		
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public Task<bool> Save() => throw new NotImplementedException();

	private async Task Create()
	{
		var item = new FiskalyConfig()
		{
			OperationType = Operation.Created,
			Id = string.Empty,
			ClientId = string.Empty,
			TseId = string.Empty,
		};

		await fiskalyConfigRepository.CrudAsync(item);
		await Load();
	}
}