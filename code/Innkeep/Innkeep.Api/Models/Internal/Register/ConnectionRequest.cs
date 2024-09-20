namespace Innkeep.Api.Models.Internal.Register;

public class ConnectionRequest
{
	public string Identifier { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string HostName { get; set; } = string.Empty;
}