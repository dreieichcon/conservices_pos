using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Client.Interfaces.Pos;

public interface ISalesItemService
{
	public IEnumerable<DtoSalesItem> SalesItems { get; set; }

	public event EventHandler? ItemsUpdated;

	public Task Load();
}