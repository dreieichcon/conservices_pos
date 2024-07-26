namespace Innkeep.Services.Client.Interfaces.Hardware;

public interface IHardwareService
{
	public string ClientIdentifier { get; init; }

	public string IpAddress { get; init; }
}