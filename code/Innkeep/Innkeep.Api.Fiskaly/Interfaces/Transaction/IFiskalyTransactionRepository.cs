using Innkeep.Api.Models.Fiskaly.Objects.Transaction;

namespace Innkeep.Api.Fiskaly.Interfaces.Transaction;

public interface IFiskalyTransactionRepository
{
	public Task<FiskalyTransaction> StartTransaction();

	public Task<FiskalyTransaction> UpdateTransaction();

	public Task<FiskalyTransaction> FinishTransaction();
}