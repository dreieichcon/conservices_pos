using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces.Pretix;

public interface IEventRepository : IBaseRepository<Event>
{
	public Event GetOrCreate(PretixEvent pretixEvent, Organizer organizer);
}