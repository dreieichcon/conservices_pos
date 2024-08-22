namespace Innkeep.Services.Server.Interfaces.Internal;

public interface IEventStateService
{
	public event EventHandler? EventStateUpdated;
	
	public string PretixOrganizerSlug { get; }
	
	public string PretixEventSlug { get; }
	
	public string EventCurrency { get; set; }

	public bool IsEventConfigured { get; }
}