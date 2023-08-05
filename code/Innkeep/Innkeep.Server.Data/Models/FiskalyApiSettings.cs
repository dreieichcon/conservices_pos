namespace Innkeep.Server.Data.Models;

public class FiskalyApiSettings
{
	public int Id { get; set; }
	
	public string? Key { get; set; }
	
	public string? Secret { get; set; }
	
	public string? Token { get; set; }
	
	public DateTime? TokenValidUntil { get; set; }
	
	public string TseId { get; set; }
	
	public string ClientId { get; set; }
}