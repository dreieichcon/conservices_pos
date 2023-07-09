namespace Innkeep.Server.Data.Models;

public class ApplicationSetting
{
	public int Id { get; set; }
	
	public Organizer? SelectedOrganizer { get; set; }
	
	public Event? SelectedEvent { get; set; }
}