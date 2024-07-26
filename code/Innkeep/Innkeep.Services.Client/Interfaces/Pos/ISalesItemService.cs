using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Client.Interfaces.Pos;

public interface ISalesItemService
{
	public event EventHandler? ItemsUpdated;
	
	public IList<DtoSalesItem> SalesItems { get; set; }

	public Task Load();
}