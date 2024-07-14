using Innkeep.Server.Db.Models;

namespace Innkeep.Server.Services.Interfaces.Registers;

public interface IRegisterService
{
	public event EventHandler? PendingRegisterAdded;

	public event EventHandler? ItemsUpdated;
	
	public List<Register> KnownRegisters { get; set; }
	
	public List<Register> PendingRegisters { get; set; }
	
	public bool IsKnown(string registerIdentifier);

	public void AddPending(string registerIdentifier, string registerDescription);

	public Task Load();

	public Task Save();

	public Task AddToKnown(string registerIdentifier);
}