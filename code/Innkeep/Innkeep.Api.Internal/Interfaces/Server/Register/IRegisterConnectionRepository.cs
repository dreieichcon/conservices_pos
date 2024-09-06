namespace Innkeep.Api.Internal.Interfaces.Server.Register;

public interface IRegisterConnectionRepository
{
	public Task<bool> Test();

	public Task<bool> Connect(string identifier, string description, string ip);

	public Task<bool> Discover(string address);
}