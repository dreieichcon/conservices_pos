using Innkeep.Db.Server.Models.Config;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Server.Interfaces.Internal;

namespace Innkeep.Services.Server.Internal;

public class EventStateService : IEventStateService
{
	private readonly IDbService<PretixConfig> _pretixConfigService;

	public EventStateService(IDbService<PretixConfig> pretixConfigService)
	{
		_pretixConfigService = pretixConfigService;

		_pretixConfigService.ItemsUpdated += (_, _) => EventStateUpdated?.Invoke(this, EventArgs.Empty);
	}

	public event EventHandler? EventStateUpdated;

	public string PretixOrganizerSlug => _pretixConfigService.CurrentItem!.SelectedOrganizerSlug ?? string.Empty;

	public string PretixEventSlug => _pretixConfigService.CurrentItem!.SelectedEventSlug ?? string.Empty;

	public string EventCurrency { get; set; }

	public bool IsEventConfigured
		=> !string.IsNullOrEmpty(PretixOrganizerSlug) && !string.IsNullOrEmpty(PretixEventSlug);
}