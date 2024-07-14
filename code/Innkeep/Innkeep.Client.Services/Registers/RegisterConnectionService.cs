using Innkeep.Api.Server.Interfaces;
using Innkeep.Client.Services.Interfaces.Register;

namespace Innkeep.Client.Services.Registers;

public class RegisterConnectionService(IRegisterConnectionRepository repository) : IRegisterConnectionService
{
	public async Task<bool> Test()
	{
		return await repository.Test();
	}
}