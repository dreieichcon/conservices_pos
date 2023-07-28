namespace Innkeep.Client.Interfaces.Services;

public interface ISerialPortRepository
{
	public List<string> GetPorts();
}