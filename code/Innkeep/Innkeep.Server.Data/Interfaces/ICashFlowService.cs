using Innkeep.Core.DomainModels.Dashboard;
using Innkeep.Server.Data.Models;
using Transaction = Innkeep.Shared.Objects.Transaction.Transaction;

namespace Innkeep.Server.Data.Interfaces;

public interface ICashFlowService
{
	public void CreateCashFlow(Register register, Transaction transaction);

	public List<RegisterCashInfo> GetCurrentCashState();
}