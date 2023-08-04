using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories;

public class OrganizerRepository : BaseRepository<Organizer>, IOrganizerRepository
{
	public Organizer GetOrCreate(PretixOrganizer pretixOrganizer, DbContext db)
	{
		var fromDb = GetCustom(x => x.Slug == pretixOrganizer.Slug, db);
		if (fromDb is not null) return fromDb;

		var toDb = new Organizer()
		{
			Name = pretixOrganizer.Name,
			Slug = pretixOrganizer.Slug
		};

		Create(toDb, db);
		return GetCustom(x => x.Slug == pretixOrganizer.Slug)!;
	}
}