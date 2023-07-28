namespace Innkeep.Client.Interfaces.Services;

public interface IClientServerConnectionService
{
	public Task<bool> TestConnection();

	public Task<bool> RegisterToServer();

	public bool AutoDiscover(out Uri? uri);
}