using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;

namespace Innkeep.Client.Services.Legacy.Interfaces.Server;

public interface IClientServerConnectionService
{
	public Task<bool> TestConnection();

	public Task<bool> RegisterToServer();

	public bool AutoDiscover(out Uri? uri);
	public Task<Receipt?> SendTransaction(PretixTransaction transaction);

	public Task<PretixCheckinResponse?> SendCheckIn(PretixCheckin checkin);
}