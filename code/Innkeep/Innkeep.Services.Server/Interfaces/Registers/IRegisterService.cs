using Innkeep.Db.Server.Models;

namespace Innkeep.Services.Server.Interfaces.Registers;

public interface IRegisterService
{
	public event EventHandler? PendingRegisterAdded;

	public event EventHandler? ItemsUpdated;
	
	public List<Register> KnownRegisters { get; set; }
	
	public List<Register> PendingRegisters { get; set; }
	
	public bool IsKnown(string registerIdentifier);

	public Task Update(string registerIdentifier, string registerDescription, string registerIp);

	public void AddPending(string registerIdentifier, string registerDescription);

	public Task Load();

	public Task Save();

	public Task AddToKnown(string registerIdentifier);

	public Task Delete(string identifier);

	public Task ReloadConnected();

	public Task<string> GetAddress(string clientId, bool reload = true);
}