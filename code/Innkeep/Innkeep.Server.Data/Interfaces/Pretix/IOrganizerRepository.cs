using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces;

public interface IOrganizerRepository : IBaseRepository<Organizer>
{
	public Organizer GetOrCreate(PretixOrganizer pretixOrganizer, DbContext db);
}