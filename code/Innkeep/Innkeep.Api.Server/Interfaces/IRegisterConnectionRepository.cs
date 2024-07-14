namespace Innkeep.Api.Server.Interfaces;

public interface IRegisterConnectionRepository
{
	public Task<bool> Test();

	public Task<bool> Connect();

	public Task<bool> Discover();
}