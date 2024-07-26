namespace Innkeep.Services.Client.Interfaces.Registers;

public interface IRegisterConnectionService
{
	public event EventHandler? TestAddressChanged;
	
	public string CurrentTestAddress { get; set; }
	
	public Task<bool> Test();

	public Task<string?> Discover(CancellationToken token);

	public Task<bool> Connect(string description);
}