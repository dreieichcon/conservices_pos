using Innkeep.Db.Enum;
using Innkeep.Db.Interfaces;
using Innkeep.Db.Server.Models;
using Innkeep.Server.Services.Interfaces.Registers;

namespace Innkeep.Server.Services.Registers;

public class RegisterService(IDbRepository<Register> registerRepository) : IRegisterService
{
	public event EventHandler? PendingRegisterAdded;

	public event EventHandler? ItemsUpdated;

	public List<Register> KnownRegisters { get; set; } = [];

	public List<Register> PendingRegisters { get; set; } = [];
	
	public bool IsKnown(string registerIdentifier) =>
		KnownRegisters.Any(x => x.RegisterIdentifier == registerIdentifier);

	public void AddPending(string registerIdentifier, string registerDescription)
	{
		PendingRegisters.Add(
			new Register()
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
}