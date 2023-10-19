using Innkeep.Models.Transaction;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Services.Models;

namespace Innkeep.Server.Services.Interfaces.Api;

public interface IFiskalyService
{
	public Task<TseResult?> CreateTransaction(PretixTransaction transaction);

	public Task<TseResult?> CreateCashFlow(CashFlow cashFlow);
}