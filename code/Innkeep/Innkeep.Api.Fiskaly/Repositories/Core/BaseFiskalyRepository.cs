using System.Text;
using System.Text.Json;
using Innkeep.Api.Auth;
using Innkeep.Api.Core.Http;
using Innkeep.Api.Json;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public class BaseFiskalyRepository(IFiskalyAuthenticationService authenticationService) : BaseHttpRepository
{
	protected IFiskalyAuthenticationService AuthenticationService => authenticationService;
	protected override HttpContent CreatePostMessage(string content)
		=> new StringContent(content, Encoding.UTF8, "application/json");
	
	protected override HttpContent CreatePutMessage(string content) => throw new NotImplementedException();

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		throw new NotImplementedException();
	}

	protected override void InitializePostHeaders()
	{
		// nothing yet
	}

	protected override void InitializePutHeaders()
	{
		throw new NotImplementedException();
	}

	protected override async Task PrepareRequest() 
		=> await authenticationService.GetOrUpdateToken();
	
	

	protected override JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			Converters =
			{
				new FiskalyDateTimeJsonConverter(),
			},
		};
	}
}