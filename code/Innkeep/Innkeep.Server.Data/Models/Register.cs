namespace Innkeep.Server.Data.Models;

public class Register
{
	public int Id { get; set; }

	public required string DeviceId { get; set; }

	public string RegisterName { get; set; } = string.Empty;

	public override bool Equals(object? obj)
	{
		if (obj is Register register)
			return DeviceId == register.DeviceId;

		return false;
	}

	public override int GetHashCode()
	{
		return DeviceId.ToCharArray().Sum(x => x);
	}
}