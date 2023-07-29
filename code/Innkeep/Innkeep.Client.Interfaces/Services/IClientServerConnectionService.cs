using Innkeep.Core.DomainModels.Print;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientServerConnectionService
{
	public Task<bool> TestConnection();

	public Task<bool> RegisterToServer();

	public bool AutoDiscover(out Uri? uri);
	public Task<Receipt?> SendTransaction(Transaction transaction);
}