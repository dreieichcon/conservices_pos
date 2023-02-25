using Innkeep.Core.Interfaces.Services;

namespace Innkeep.DI.Services;

public class ApplicationSettingsService : IApplicationSettingsService
{
    public ApplicationSettingsService()
    {
        RetrieveSettings();
    }
    
    public void RetrieveSettings()
    {
        SelectedOrganizerSetting = "ktv-ticket";
        SelectedEventSetting = "c8vub";
    }

    public string SelectedOrganizerSetting { get; set; }
    
    public string SelectedEventSetting { get; set; }
}