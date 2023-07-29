using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Innkeep.Client.Interfaces.Services;
using Innkeep.Core.Core;
using Innkeep.Core.DomainModels.Print;
using Innkeep.Core.Validation;
using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Api.ServerEndpointBuilder;
using Innkeep.Shared.Objects.Transaction;
using Serilog;

namespace Innkeep.DI.Services.Client.Core;

public class ClientServerConnectionRepository : BaseHttpRepository, IClientServerConnectionRepository
{
	private readonly IClientSettingsService _clientSettingsService;
	private readonly INetworkHardwareService _networkHardwareService;

	private string _registerId = string.Empty;

	public ClientServerConnectionRepository(IClientSettingsService clientSettingsService, 
											INetworkHardwareService networkHardwareService)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		_clientSettingsService = clientSettingsService;
		_networkHardwareService = networkHardwareService;

		Client = new HttpClient(
			new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = ServerValidator.ValidateServerCertificate
			}
		);
	}

	public async Task<bool> TestConnection(Uri uri)
	{
		var endpoint = new ServerEndpointBuilder(uri)
						.WithRegister()
						.WithEndpoint("Discover")
						.Build();

		try
		{
			using var message = CreateGetMessage(endpoint);
			var result = await ExecuteGetRequest(message);
			if (!string.IsNullOrEmpty(result)) return true;
		}
		catch (Exception ex)
		{
			return false;
		}

		return false;
	}

	public async Task<bool> RegisterToServer()
	{
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithRegister()
						.WithEndpoint("Connect")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();
		
		try
		{
			using var message = CreateGetMessage(endpoint);
			var result = await ExecuteGetRequest(message);

			if (!string.IsNullOrEmpty(result))
			{
				_registerId = result;
				return true;
			}
		}
		catch (Exception ex)
		{
			return false;
		}

		return false;
	}

	public async Task<PretixOrganizer> GetOrganizer()
	{
		if (string.IsNullOrEmpty(_registerId))
		{
			throw new AuthenticationException("Register has not been connected to server.");
		}
		
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithPretix()
						.WithEndpoint("Organizer")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();

		try
		{
			using var message = CreateGetMessage(endpoint);
			var result = await ExecuteGetRequest(message);

			return JsonSerializer.Deserialize<PretixOrganizer>(result);
		}
		catch (Exception ex)
		{
			Log.Error("Failed while retrieving Organizer: {Exception}", ex);
			throw new AuthenticationException(ex.Message);
		}
	}

	public async Task<PretixEvent> GetEvent()
	{
		if (string.IsNullOrEmpty(_registerId))
		{
			throw new AuthenticationException("Register has not been connected to server.");
		}
		
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithPretix()
						.WithEndpoint("Event")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();

		try
		{
			using var message = CreateGetMessage(endpoint);
			var result = await ExecuteGetRequest(message);

			return JsonSerializer.Deserialize<PretixEvent>(result);
		}
		catch (Exception ex)
		{
			Log.Error("Failed while retrieving Event: {Exception}", ex);
			throw new AuthenticationException(ex.Message);
		}
	}

	public async Task<IEnumerable<PretixSalesItem>> GetSalesItems()
	{
		if (string.IsNullOrEmpty(_registerId))
		{
			throw new AuthenticationException("Register has not been connected to server.");
		}
		
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithPretix()
						.WithEndpoint("SalesItems")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();

		try
		{
			using var message = CreateGetMessage(endpoint);
			var result = await ExecuteGetRequest(message);

			return JsonSerializer.Deserialize<List<PretixSalesItem>>(FormatDecimals(result));
		}
		catch (Exception ex)
		{
			Log.Error("Failed while retrieving SalesItems: {Exception}", ex);
			throw new AuthenticationException(ex.Message);
		}
	}

	public async Task<Receipt?> SendTransaction(Transaction transaction)
	{
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithPretix()
						.WithEndpoint("Transaction")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();
		
		try
		{
			var json = JsonSerializer.Serialize(transaction);
			var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
			
			var result = await ExecutePostRequest(endpoint, jsonContent);

			var deserialized = JsonSerializer.Deserialize<Receipt?>(result);
			return deserialized;
		}
		catch (Exception ex)
		{
			Log.Error("Failed while committing Transaction: {Exception}", ex);
			return null;
		}
	}

	private string FormatDecimals(string input)
	{
		var re = new Regex(@"(?![0-9]+)\.(?=[0-9]+)");
		return re.Replace(input, ",");
	}

	protected override void PrepareGetHeaders(HttpRequestMessage message)
	{
		// do nothing
	}

	protected override void PreparePostHeaders()
	{
		// do nothing
	}
	
	
}