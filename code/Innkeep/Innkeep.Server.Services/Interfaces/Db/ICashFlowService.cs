using Innkeep.Server.Data.Models;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Interfaces.Db;

public interface ICashFlowService
{
	public void CreateCashFlow(Register register, PretixTransaction transaction);

	public void CreateCashFlow(CashFlow cashFlow);

	public IEnumerable<IGrouping<Register, CashFlow>> GetCashFlows();
}