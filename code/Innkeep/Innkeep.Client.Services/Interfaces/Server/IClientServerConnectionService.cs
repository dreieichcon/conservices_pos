using System.Transactions;
using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;

namespace Innkeep.Client.Services.Interfaces.Server;

public interface IClientServerConnectionService
{
	public Task<bool> TestConnection();

	public Task<bool> RegisterToServer();

	public bool AutoDiscover(out Uri? uri);
	public Task<Receipt?> SendTransaction(PretixTransaction transaction);
}