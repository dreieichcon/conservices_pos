namespace Innkeep.Server.Data.Models;

public class Organizer
{
	public int Id { get; set; }
	
	public required string Name { get; set; }

	public required string Slug { get; set; }
}