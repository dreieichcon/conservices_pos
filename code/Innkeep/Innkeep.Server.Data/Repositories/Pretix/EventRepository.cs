using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces.Pretix;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories.Pretix;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
	public Event GetOrCreate(PretixEvent pretixEvent, Organizer organizer)
	{
		var db = InnkeepServerContext.Create();
		
		var fromDb = GetCustom(x => x.Slug == pretixEvent.Slug);
		if (fromDb is not null) return fromDb;

		db.Attach(organizer);
		var toDb = new Event()
		{
			Organizer = organizer,
			Name = pretixEvent.Name.German,
			Slug = pretixEvent.Slug
		};

		Create(toDb, db);
		return GetCustom(x => x.Slug == pretixEvent.Slug)!;
	}
}