using Innkeep.Server.Data.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Interfaces.Db;

public interface ICashFlowService
{
	public void CreateTransactionCashFlow(Register register, PretixTransaction transaction);

	public Task CreateServerCashFlow(CashFlow cashFlow);

	public IEnumerable<IGrouping<Register, CashFlow>> GetCashFlows();
}