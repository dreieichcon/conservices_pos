using Innkeep.Core.DomainModels.KassSichV;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.Server.Interfaces.Services;

public interface ITseService
{
	public Task<TseResult> CreateEntry(Transaction transaction);

}