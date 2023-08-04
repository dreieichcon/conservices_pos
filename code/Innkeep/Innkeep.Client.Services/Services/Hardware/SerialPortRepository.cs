using System.IO.Ports;
using Innkeep.Client.Services.Interfaces.Hardware;

namespace Innkeep.Client.Services.Services.Hardware;

public class SerialPortRepository : ISerialPortRepository
{
	public List<string> GetPorts()
	{
		var ports = SerialPort.GetPortNames();
		return ports.ToList();
	}
}