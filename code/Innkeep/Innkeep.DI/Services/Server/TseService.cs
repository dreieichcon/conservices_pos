using Innkeep.Core.DomainModels.KassSichV;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.DI.Services.Server;

public class TseService : ITseService
{
	public async Task<TseResult> CreateEntry(Transaction transaction)
	{
		return new TseResult();
	}
}