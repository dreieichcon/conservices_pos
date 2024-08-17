using Innkeep.Api.Server.Interfaces;
using Innkeep.Services.Client.Interfaces.Hardware;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Services.Client.Interfaces.Registers;
using Innkeep.Services.Interfaces.Hardware;
using Serilog;

namespace Innkeep.Services.Client.Registers;

public class RegisterConnectionService : IRegisterConnectionService
{
	private string _currentTestAddress = string.Empty;
	private readonly IRegisterConnectionRepository _repository;
	private readonly IHardwareService _hardwareService;
	private readonly IEventRouter _router;

	public RegisterConnectionService(IRegisterConnectionRepository repository, IHardwareService hardwareService, IEventRouter router)
	{
		_repository = repository;
		_hardwareService = hardwareService;
		_router = router;
	}

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
		var identifier = _hardwareService.ClientIdentifier;
		var ip = _hardwareService.HostName;

		var result = await _repository.Connect(identifier, description, ip);
		
		if (result)
			_router.Connected();

		return result;
	}

	public async Task<bool> Test()
	{
		return await _repository.Test();
	}

	public async Task<string?> Discover(CancellationToken token)
	{
		// first get the ip address of the client
		var address = _hardwareService.IpAddress;

		var addressModifiable = address.Split(".").Take(3);
		var iterable = string.Join(".", addressModifiable);
		
		// first try all 256 ips of the client address
		for (var i = 1; i < 256; i++)
		{
			if (token.IsCancellationRequested)
				return null;
			
			CurrentTestAddress = $"{ServerProtocol}{iterable}.{i}:{ServerPort}";

			Log.Debug("Testing {TestAddress}", CurrentTestAddress);
			
			if (await _repository.Discover(CurrentTestAddress))
			{
				return CurrentTestAddress;
			}
		}

		// then try localhost
		if (await _repository.Discover(Localhost))
			return Localhost;
		
		return null;
	}
	
}