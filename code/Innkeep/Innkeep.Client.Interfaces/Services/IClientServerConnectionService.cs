namespace Innkeep.Client.Interfaces.Services;

public interface IClientServerConnectionService
{
	public bool TestConnection();

	public bool AutoDiscover(out Uri? uri);
}