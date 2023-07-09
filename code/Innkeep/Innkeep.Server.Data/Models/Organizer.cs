namespace Innkeep.Server.Data.Models;

public class Organizer
{
	public int Id { get; set; }
	
	public string Name { get; set; }

	public string Slug { get; set; } = null!;
}