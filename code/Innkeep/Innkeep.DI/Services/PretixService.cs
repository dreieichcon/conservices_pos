using Innkeep.Core.DomainModels.Pretix;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.Core.Interfaces.Services;

namespace Innkeep.DI.Services;

public class PretixService : IPretixService
{
    private IPretixRepository _pretixRepository;
    private readonly IApplicationSettingsService _applicationSettingsService;

    private PretixOrganizer? _selectedOrganizer;
    private PretixEvent? _selectedEvent;

    public PretixService(IPretixRepository pretixRepository, IApplicationSettingsService applicationSettingsService)
    {
        _pretixRepository = pretixRepository;
        _applicationSettingsService = applicationSettingsService;
        SalesItems = new List<PretixSalesItem>();
        Initialize();
        LoadFromSettings();
    }

    private void Initialize()
    {
        Organizers = Task.Run(() => _pretixRepository.GetOrganizers()).Result;
    }

    private void LoadFromSettings()
    {
        SelectedOrganizer = Organizers.FirstOrDefault(x => x.Slug == _applicationSettingsService.SelectedOrganizerSetting);
        SelectedEvent = Events.FirstOrDefault(x => x.Slug == _applicationSettingsService.SelectedEventSetting);
        Initialized?.Invoke(null, EventArgs.Empty);
        ItemUpdated?.Invoke("SalesItems", EventArgs.Empty);
    }

    public IEnumerable<PretixOrganizer> Organizers { get; set; }
    
    public IEnumerable<PretixEvent> Events { get; set; }

    public PretixOrganizer? SelectedOrganizer
    {
        get => _selectedOrganizer;

        set
        {
            _selectedOrganizer = value;
            ItemUpdated?.Invoke("SelectedOrganizer", EventArgs.Empty);

            if (_selectedOrganizer is null) return;
            
            Events = Task.Run(() => _pretixRepository.GetEvents(_selectedOrganizer)).Result;

            ItemUpdated?.Invoke("Events", EventArgs.Empty);

        }
    }

    public PretixEvent? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            ItemUpdated?.Invoke("SelectedEvent", EventArgs.Empty);

            if (_selectedEvent is null) return;
            if (_selectedOrganizer is null) return;

            SalesItems = Task.Run(() => _pretixRepository.GetItems(_selectedOrganizer, _selectedEvent)).Result;
            ItemUpdated?.Invoke("SalesItems", EventArgs.Empty);
        }
    }

    public IEnumerable<PretixSalesItem> SalesItems { get; set; }
    
    public event EventHandler? ItemUpdated;
    public event EventHandler? Initialized;
}