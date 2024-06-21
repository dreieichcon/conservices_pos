namespace Innkeep.Client.Services.Legacy.Interfaces.Hardware;

public interface ISerialPortRepository
{
	public List<string> GetPorts();
}