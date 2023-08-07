using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Server.Data.Interfaces.Pretix;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories.Pretix;

public class OrganizerRepository : BaseRepository<Organizer>, IOrganizerRepository
{
	public Organizer GetOrCreate(PretixOrganizer pretixOrganizer)
	{
		var fromDb = GetCustom(x => x.Slug == pretixOrganizer.Slug);
		if (fromDb is not null) return fromDb;

		var toDb = new Organizer()
		{
			Name = pretixOrganizer.Name,
			Slug = pretixOrganizer.Slug
		};

		Create(toDb);
		return GetCustom(x => x.Slug == pretixOrganizer.Slug)!;
	}
}