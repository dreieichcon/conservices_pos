using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Innkeep.Services.Interfaces.Hardware;

namespace Innkeep.Services.Hardware;

public class HardwareService : IHardwareService
{
	public HardwareService()
	{
		ClientIdentifier = GetMacAddress();
		IpAddress = GetIpAddress();
		HostName = Dns.GetHostName();
	}

	public string ClientIdentifier { get; init; }
	
	public string IpAddress { get; init; }

	public string HostName { get; init; }

	private string GetIpAddress()
	{
		var address = GetNic()
					?.GetIPProperties()
					.UnicastAddresses.FirstOrDefault(x => x.Address.AddressFamily == AddressFamily.InterNetwork);
		
		return address?.Address.ToString() ?? "127.0.0.1";
	}

	private static string GetMacAddress()
	{
		return GetNic()?.GetPhysicalAddress().ToString() ?? "NO IDENTIFIER";
	}

	private static NetworkInterface? GetNic()
	{
		return NetworkInterface
				.GetAllNetworkInterfaces()
				.FirstOrDefault(
					nic => nic.OperationalStatus == OperationalStatus.Up &&
							nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
				);
	}
}