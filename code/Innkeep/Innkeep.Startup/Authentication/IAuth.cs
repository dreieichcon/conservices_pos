namespace Innkeep.Startup.Authentication;

public interface IAuth
{
	public ulong WebhookId { get; }
	public string WebhookToken { get; }
	
	public string WebhookUrl { get; }
}