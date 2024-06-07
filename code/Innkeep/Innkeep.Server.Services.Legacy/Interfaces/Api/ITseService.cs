using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Legacy.Models;

namespace Innkeep.Server.Services.Legacy.Interfaces.Api;

public interface ITseService
{
	public Task<TseResult?> CreateEntry(PretixTransaction transaction);

}