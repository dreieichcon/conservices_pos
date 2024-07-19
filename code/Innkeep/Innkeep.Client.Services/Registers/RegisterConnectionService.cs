using Innkeep.Api.Server.Interfaces;
using Innkeep.Client.Services.Interfaces.Hardware;
using Innkeep.Client.Services.Interfaces.Registers;
using Serilog;

namespace Innkeep.Client.Services.Registers;

public class RegisterConnectionService(IRegisterConnectionRepository repository, IHardwareService hardwareService) : IRegisterConnectionService
{
	private string _currentTestAddress = string.Empty;
	
	private const string ServerPort = "1337";

	private const string ServerProtocol = "https://";
	
	private const string Localhost = $"{ServerProtocol}localhost:{ServerPort}";

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
		var ip = hardwareService.IpAddress;

		return await repository.Connect(identifier, description, ip);
	}

	public async Task<bool> Test()
	{
		return await repository.Test();
	}

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
			
			if (await repository.Discover(CurrentTestAddress))
			{
				return CurrentTestAddress;
			}
		}

		// then try localhost
		if (await repository.Discover(Localhost))
			return Localhost;
		
		return null;
	}
	
}