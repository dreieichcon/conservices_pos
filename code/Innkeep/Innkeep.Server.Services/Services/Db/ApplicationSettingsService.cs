using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Services.Interfaces.Db;
using Microsoft.EntityFrameworkCore;

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
        var db = _organizerRepository.CreateContext();
        
        var organizerFromDb = _organizerRepository.GetOrCreate(pretixOrganizer, db);
        var eventFromDb = _eventRepository.GetOrCreate(pretixEvent, organizerFromDb, db);

        ActiveSetting.SelectedOrganizer = organizerFromDb;
        ActiveSetting.SelectedEvent = eventFromDb;
        ActiveSetting.OrganizerInfo = organizerInfo;
        
        Save(db);
        db.Dispose();
    }

    public void Save(DbContext db)
    {
        _applicationSettingsRepository.Update(ActiveSetting, db);
        Load();
    }
    
    public ApplicationSetting ActiveSetting { get; set; }
}