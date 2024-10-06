using Demolite.Db.Enum;
using Demolite.Db.Interfaces;
using Innkeep.Api.Internal.Interfaces.Client.Actions;
using Innkeep.Db.Server.Models.Config;
using Innkeep.Services.Server.Interfaces.Registers;

namespace Innkeep.Services.Server.Registers;

public class RegisterService(IDbRepository<Register> registerRepository, IClientReloadRepository reloadRepository)
	: IRegisterService
{
	public event EventHandler? PendingRegisterAdded;

	public event EventHandler? ItemsUpdated;

	public List<Register> KnownRegisters { get; set; } = [];

	public List<Register> PendingRegisters { get; set; } = [];

	public bool IsKnown(string registerIdentifier)
		=> KnownRegisters.Any(x => x.RegisterIdentifier == registerIdentifier);

	public async Task Update(string registerIdentifier, string registerDescription, string registerIp)
	{
		var register = KnownRegisters.First(x => x.RegisterIdentifier == registerIdentifier);
		register.RegisterDescription = registerDescription;
		register.LastHostname = registerIp;
		register.OperationType = Operation.Updated;

		await Save();
		await Load();
	}

	public void AddPending(string registerIdentifier, string registerDescription)
	{
		PendingRegisters.Add(
			new Register
			{
				RegisterIdentifier = registerIdentifier,
				RegisterDescription = registerDescription,
				OperationType = Operation.Created,
			}
		);

		PendingRegisterAdded?.Invoke(this, EventArgs.Empty);
	}

	public async Task Load()
	{
		KnownRegisters = (await registerRepository.GetAllAsync()).ToList();
	}

	public async Task Save()
	{
		await registerRepository.CrudManyAsync(KnownRegisters);
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task AddToKnown(string registerIdentifier)
	{
		var register = PendingRegisters.First(x => x.RegisterIdentifier == registerIdentifier);

		PendingRegisters.Remove(register);
		KnownRegisters.Add(register);

		await Save();
		await Load();
	}

	public async Task Delete(string identifier)
	{
		var register = Retrieve(identifier);
		register.OperationType = Operation.Removed;

		await Save();
		await Load();
	}

	public async Task ReloadConnected()
	{
		foreach (var register in KnownRegisters.Where(x => !string.IsNullOrEmpty(x.LastHostname)))
		{
			var address = await GetAddress(register.Id, false);
			await reloadRepository.Reload(register.RegisterIdentifier, address);
		}
	}

	public async Task<string> GetAddress(string clientId, bool reload = true)
	{
#if DEBUG
		return "https://localhost:42069";

#elif RELEASE
		var register = KnownRegisters.FirstOrDefault(x => x.RegisterIdentifier == clientId);

		if (register is null) return string.Empty;
		
		var address = $"https://{register.LastHostname}:42069";
		return address;
# endif
	}

	private Register Retrieve(string identifier)
	{
		return KnownRegisters.First(x => x.RegisterIdentifier == identifier);
	}
}