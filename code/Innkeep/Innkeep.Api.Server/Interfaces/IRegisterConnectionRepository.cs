namespace Innkeep.Api.Server.Interfaces;

public interface IRegisterConnectionRepository
{
	public Task<bool> Test();

	public Task<bool> Connect(string identifier, string description, string ip);

	public Task<bool> Discover(string address);
}