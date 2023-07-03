namespace Innkeep.Server.Data.Models;

public class Event
{
	public int Id { get; set; }
	
	public string EventSlug { get; set; }
	
	public string OrganizerSlug { get; set; }
	
	public IEnumerable<Register> AttachedRegisters { get; set; }
}