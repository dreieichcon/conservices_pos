namespace Innkeep.Api.Fiskaly.Tests.Data;

public interface ITestAuth
{
	public string FiskalyApiKey { get; }
	
	public string FiskalyApiSecret { get; }
}