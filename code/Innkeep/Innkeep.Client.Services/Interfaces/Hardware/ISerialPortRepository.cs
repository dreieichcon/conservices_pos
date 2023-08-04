namespace Innkeep.Client.Services.Interfaces.Hardware;

public interface ISerialPortRepository
{
	public List<string> GetPorts();
}