namespace Innkeep.Client.Services.Interfaces.Register;

public interface IRegisterConnectionService
{
	public Task<bool> Test();
}