using Innkeep.Api.Internal.Interfaces.Server.Register;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Services.Client.Interfaces.Registers;
using Innkeep.Services.Interfaces.Hardware;
using Serilog;

namespace Innkeep.Services.Client.Registers;

public class RegisterConnectionService(
	IRegisterConnectionRepository repository,
	IHardwareService hardwareService,
	IEventRouter router
) : IRegisterConnectionService
{
	private const string ServerPort = "1337";
	private const string ServerProtocol = "https://";
	private const string Localhost = $"{ServerProtocol}localhost:{ServerPort}";

	private string _currentTestAddress = string.Empty;

	public event EventHandler? TestAddressChanged;

	public string CurrentTestAddress
	{
		get => _currentTestAddress;
		set
		{
			_currentTestAddress = value;
			TestAddressChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public async Task<bool> Connect(string description)
	{
		var identifier = hardwareService.ClientIdentifier;
		var ip = hardwareService.HostName;

		var result = await repository.Connect(identifier, description, ip);

		if (result.IsSuccess)
			router.Connected();

		return result.IsSuccess;
	}

	public async Task<bool> Test() => (await repository.Test()).IsSuccess;

	public async Task<string?> Discover(CancellationToken token)
	{
		// first get the ip address of the client
		var address = hardwareService.IpAddress;

		var addressModifiable = address.Split(".").Take(3);
		var iterable = string.Join(".", addressModifiable);

		// first try all 256 ips of the client address
		for (var i = 1; i < 256; i++)
		{
			if (token.IsCancellationRequested)
				return null;

			CurrentTestAddress = $"{ServerProtocol}{iterable}.{i}:{ServerPort}";

			Log.Debug("Testing {TestAddress}", CurrentTestAddress);

			if ((await repository.Discover(CurrentTestAddress)).IsSuccess) return CurrentTestAddress;
		}

		// then try localhost
		if ((await repository.Discover(Localhost)).IsSuccess)
			return Localhost;

		return null;
	}
}