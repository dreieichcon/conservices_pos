using System.IO.Ports;
using Innkeep.Client.Services.Legacy.Interfaces.Hardware;

namespace Innkeep.Client.Services.Legacy.Services.Hardware;

public class SerialPortRepository : ISerialPortRepository
{
	public List<string> GetPorts()
	{
		var ports = SerialPort.GetPortNames();
		return ports.ToList();
	}
}