namespace Innkeep.Client.Interfaces.Services;

public interface INetworkHardwareService
{
	public string MacAddress { get; set; }
	public string GetMacAddress();
}