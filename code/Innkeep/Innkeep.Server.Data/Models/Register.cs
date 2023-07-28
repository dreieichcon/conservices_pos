namespace Innkeep.Server.Data.Models;

public class Register
{
	public int Id { get; set; }

	public string DeviceId { get; set; } = null!;

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