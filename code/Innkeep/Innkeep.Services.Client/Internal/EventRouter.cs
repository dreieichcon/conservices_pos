using Innkeep.Services.Client.Interfaces.Internal;

namespace Innkeep.Services.Client.Internal;

public class EventRouter : IEventRouter
{
	public event EventHandler? OnRegisterConnected;

	public void Connected() => OnRegisterConnected?.Invoke(this, EventArgs.Empty);

	public event EventHandler? OnSalesItemsReloaded;

	public void SalesItemsReloaded() => OnSalesItemsReloaded?.Invoke(this, EventArgs.Empty);
}