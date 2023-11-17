using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Innkeep.Api.Pretix.Endpoints;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Api.Pretix.Serialization;
using Innkeep.Http;
using Innkeep.Json;
using Serilog;

namespace Innkeep.Api.Pretix.Repositories;

public class PretixRepository : BaseHttpRepository, IPretixRepository
{
	private readonly IAuthenticationService _authenticationService;

	private string Token => _authenticationService.AuthenticationInfo.PretixToken;

	public PretixRepository(IAuthenticationService authenticationService)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		_authenticationService = authenticationService;

		try
		{
			var result = Task.Run(TestConnection).Result;
			if (result != null) _authenticationService.AuthenticationSuccessful = true;
		}
		catch
		{
			_authenticationService.AuthenticationSuccessful = false;
		}
	}

	public async Task<string?> TestConnection()
	{
		using var message = new HttpRequestMessage(
			HttpMethod.Get,
			new PretixEndpointBuilder().WithEndpoint("organizers").Build()
		);

		PrepareGetHeaders(message);

		var response = await Client.SendAsync(message);

		return await response.Content.ReadAsStringAsync();
	}

	public async Task<IEnumerable<PretixOrganizer>> GetOrganizers()
	{
		using var message = new HttpRequestMessage(
			HttpMethod.Get,
			new PretixEndpointBuilder().WithEndpoint("organizers").Build()
		);

		var content = await ExecuteGetRequest(message);

		var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixOrganizer>>(content);

		return deserialized!.Results;
	}

	public async Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer)
	{
		using var message = CreateGetMessage(
			new PretixEndpointBuilder().WithOrganizer(organizer).WithEndpoint("events").Build()
		);

		var content = await ExecuteGetRequest(message);

		var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixEvent>>(content);

		if (deserialized is null)
		{
			Log.Debug("Received null response for PretixEvents for {Organizer}", organizer.Name);
		}
		return deserialized != null ? deserialized.Results : new List<PretixEvent>();
	}

	public async Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer organizer, PretixEvent pretixEvent)
	{
		using var message = new HttpRequestMessage(
			HttpMethod.Get,
			new PretixEndpointBuilder().WithOrganizer(organizer).WithEvent(pretixEvent).WithEndpoint("items").Build()
		);

		var content = await ExecuteGetRequest(message);

		var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixSalesItem>>(content, new JsonSerializerOptions()
		{
			Converters = { new DecimalJsonConverter() }
		});

		if (deserialized is null)
		{
			Log.Debug("Received null response for PretixSalesItem for {Organizer}, {Event}", organizer.Name, pretixEvent.Name);
			return new List<PretixSalesItem>();
		}
		
		foreach (var pretixSalesItem in deserialized.Results)
		{
			pretixSalesItem.Currency = pretixEvent.Currency;
		}

		return deserialized.Results;
	}

	public async Task<IEnumerable<PretixCheckinList>> GetCheckinLists(
		PretixOrganizer organizer,
		PretixEvent pretixEvent
	)
	{
		using var message = new HttpRequestMessage(
			HttpMethod.Get,
			new PretixEndpointBuilder().WithOrganizer(organizer).WithEvent(pretixEvent).WithEndpoint("checkinlists").Build()
		);

		var content = await ExecuteGetRequest(message);
		
		var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixCheckinList>>(content, new JsonSerializerOptions()
		{
			Converters = { new DecimalJsonConverter() }
		});
		
		if (deserialized is null)
		{
			Log.Debug("Received null response for CheckinLists for {Organizer}, {Event}", organizer.Name, pretixEvent.Name);
			return new List<PretixCheckinList>();
		}

		return deserialized.Results;
	}

	public async Task<PretixOrderResponse?> CreateOrder
	(PretixOrganizer organizer,
	PretixEvent pretixEvent,
	IEnumerable<PretixCartItem<PretixSalesItem>> cartItems,
	bool isTest = false)
	{
		var endpoint = new PretixEndpointBuilder().WithOrganizer(organizer)
												.WithEvent(pretixEvent)
												.WithEndpoint("/orders/")
												.Build();

		var json = PretixCartSerializer.SerializeTransaction(cartItems, pretixEvent, isTest);

		var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

		var response = await ExecutePostRequest(endpoint, jsonContent);

		var deserialized = JsonSerializer.Deserialize<PretixOrderResponse>(response);

		if (deserialized is not null) return deserialized;

		Log.Debug("Received null response for PretixOrderResponse for {Organizer}, {Event}", organizer.Name, pretixEvent.Name);
		return null;
	}

	public async Task<PretixCheckinResponse?> CheckIn(PretixOrganizer organizer, PretixCheckin pretixCheckin)
	{
		var endpoint = new PretixEndpointBuilder().WithOrganizer(organizer)
												.WithEndpoint("/checkinrpc/")
												.WithEndpoint("/redeem/")
												.Build();

		var json = PretixCheckinSerializer.SerializeCheckin(pretixCheckin);

		var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

		string response;
		
		try
		{
			response = await ExecutePostRequest(endpoint, jsonContent);
		}
		catch (HttpRequestException e)
		{
			response = e.Message;
		}
		

		var deserialized = JsonSerializer.Deserialize<PretixCheckinResponse>(response);

		return deserialized;
	}

	protected override void PrepareGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");
		message.Headers.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override void PreparePostHeaders()
	{
		Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override void PreparePutHeaders()
	{
		throw new NotImplementedException();
	}
}