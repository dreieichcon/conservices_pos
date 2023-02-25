namespace Innkeep.Core.Interfaces.Services;

public interface IApplicationSettingsService
{
    public void RetrieveSettings();
    
    public string SelectedOrganizerSetting { get; set; }
    
    public string SelectedEventSetting { get; set; }
}