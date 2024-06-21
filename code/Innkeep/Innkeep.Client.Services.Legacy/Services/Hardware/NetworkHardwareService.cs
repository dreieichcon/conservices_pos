using System.Net.NetworkInformation;
using Innkeep.Client.Services.Legacy.Interfaces.Hardware;
using Serilog;

namespace Innkeep.Client.Services.Legacy.Services.Hardware;

public class NetworkHardwareService : INetworkHardwareService
{
	public string MacAddress { get; set; } = string.Empty;

	/// <summary>
	/// Finds the MAC address of the NIC with maximum speed.
	/// </summary>
	/// <returns>The MAC address.</returns>
	public string GetMacAddress()
	{
		if (!string.IsNullOrEmpty(MacAddress)) return MacAddress;
		
		const int minMacAddrLength = 12;
		var macAddress = string.Empty;
		long maxSpeed = -1;

		foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
		{
			Log.Debug("Found MAC Address: {Address} Type: {Type}",nic.GetPhysicalAddress(), nic.NetworkInterfaceType);

			var tempMac = nic.GetPhysicalAddress().ToString();

			if (nic.Speed <= maxSpeed || string.IsNullOrEmpty(tempMac) || tempMac.Length < minMacAddrLength) continue;

			Log.Debug("New Max Speed = {Speed}, MAC: {Mac}",nic.Speed,tempMac);
			maxSpeed = nic.Speed;
			macAddress = tempMac;
		}

		MacAddress = macAddress;
		return macAddress;
	}
}