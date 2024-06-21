namespace Innkeep.Client.Services.Legacy.Interfaces.Hardware;

public interface INetworkHardwareService
{
	public string MacAddress { get; set; }
	public string GetMacAddress();
}