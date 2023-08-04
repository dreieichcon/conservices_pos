namespace Innkeep.Client.Services.Interfaces.Hardware;

public interface INetworkHardwareService
{
	public string MacAddress { get; set; }
	public string GetMacAddress();
}