using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Models;

namespace Innkeep.Server.Services.Interfaces.Api;

public interface ITseService
{
	public Task<TseResult> CreateEntry(PretixTransaction transaction);

}