using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Interfaces.Pretix;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Services.Interfaces.Db;

namespace Innkeep.Server.Services.Services.Db;

public class ApplicationSettingsService : IApplicationSettingsService
{
    private readonly IApplicationSettingsRepository _applicationSettingsRepository;
    private readonly IOrganizerRepository _organizerRepository;
    private readonly IEventRepository _eventRepository;

    public ApplicationSettingsService(
        IApplicationSettingsRepository applicationSettingsRepository, 
        IOrganizerRepository organizerRepository, 
        IEventRepository eventRepository)
    {
        _applicationSettingsRepository = applicationSettingsRepository;
        _organizerRepository = organizerRepository;
        _eventRepository = eventRepository;
        Load();
    }

    private void Load()
    {
        ActiveSetting = _applicationSettingsRepository.GetSetting();
    }

    public void UpdateSetting(PretixOrganizer pretixOrganizer, PretixEvent pretixEvent, string organizerInfo)
    {
        var organizerFromDb = _organizerRepository.GetOrCreate(pretixOrganizer);
        var eventFromDb = _eventRepository.GetOrCreate(pretixEvent, organizerFromDb);

        ActiveSetting.SelectedOrganizer = organizerFromDb;
        ActiveSetting.SelectedEvent = eventFromDb;
        ActiveSetting.OrganizerInfo = organizerInfo;
        
        Save();
    }

    public void Reload()
    {
        Load();
    }

    public void Save()
    {
        _applicationSettingsRepository.SaveSetting(ActiveSetting);
        Load();
    }
    
    public required ApplicationSetting ActiveSetting { get; set; }
}