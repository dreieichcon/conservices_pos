using System.Globalization;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Innkeep.Api.Client.Interfaces;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Client.Services.Interfaces.File;
using Innkeep.Client.Services.Interfaces.Hardware;
using Innkeep.Core.Core;
using Innkeep.Core.Validation;
using Innkeep.Data.Pretix.Models;
using Innkeep.Endpoints.Server;
using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;
using Serilog;

namespace Innkeep.Api.Client.Repositories;

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

	public async Task<Receipt?> SendTransaction(PretixTransaction pretixTransaction)
	{
		var endpoint = new ServerEndpointBuilder(_clientSettingsService.Setting.ServerUri)
						.WithRegister()
						.WithEndpoint("Transaction")
						.WithEndpoint(_networkHardwareService.GetMacAddress())
						.Build();
		
		try
		{
			var json = JsonSerializer.Serialize(pretixTransaction);
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