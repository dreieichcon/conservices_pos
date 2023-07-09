using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces;

public interface IEventRepository : IBaseRepository<Event>
{
	public Event GetOrCreate(PretixEvent pretixEvent, Organizer organizer, DbContext db);
}