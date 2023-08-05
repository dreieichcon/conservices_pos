namespace Innkeep.Api.Fiskaly.Models.Fiskaly;

public class Signature
{
	public string value { get; set; }
	public string algorithm { get; set; }
	public string counter { get; set; }
	public string public_key { get; set; }
}