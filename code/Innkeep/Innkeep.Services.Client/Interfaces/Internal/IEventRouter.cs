namespace Innkeep.Services.Client.Interfaces.Internal;

public interface IEventRouter
{
	public event EventHandler? OnRegisterConnected;

	public void Connected();

	public event EventHandler? OnSalesItemsReloaded;
	
	public void SalesItemsReloaded();
}