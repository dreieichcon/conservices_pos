namespace Innkeep.Api.Fiskaly.Tests.Data;

public interface ITestAuth
{
	public string FiskalyApiKey { get; }
	
	public string FiskalyApiSecret { get; }
	
	public string FiskalyTestTssId { get; }
	
	public string FiskalyTestClientId { get; }
}