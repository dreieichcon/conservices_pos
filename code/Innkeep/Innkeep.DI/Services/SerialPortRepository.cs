using System.IO.Ports;
using Innkeep.Client.Interfaces.Services;

namespace Innkeep.DI.Services;

public class SerialPortRepository : ISerialPortRepository
{
	public List<string> GetPorts()
	{
		var ports = SerialPort.GetPortNames();
		return ports.ToList();
	}
}