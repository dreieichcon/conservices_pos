using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Innkeep.Core.Core;
using Innkeep.Core.Interfaces;
using Innkeep.Data.Pretix.Models;
using Innkeep.Data.Pretix.Serialization;
using Innkeep.Server.Pretix.Connectors;
using Innkeep.Server.Pretix.Interfaces;
using RestSharp;

namespace Innkeep.Server.Pretix.Repositories;

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
        using var message = new HttpRequestMessage(HttpMethod.Get, new PretixEndpointBuilder().WithEndpoint("organizers").Build());
        
        PrepareGetHeaders(message);

        var response = await Client.SendAsync(message);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<IEnumerable<PretixOrganizer>> GetOrganizers()
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, 
            new PretixEndpointBuilder().WithEndpoint("organizers").Build());
        
        var content = await ExecuteGetRequest(message);

        var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixOrganizer>>(content);

        return deserialized!.Results;
    }

    public async Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer)
    {
        using var message = CreateGetMessage(new PretixEndpointBuilder()
                                             .WithOrganizer(organizer)
                                             .WithEndpoint("events").Build());

        var content = await ExecuteGetRequest(message);
            
        var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixEvent>>(content);

        return deserialized.Results;
    }

    public async Task<IEnumerable<PretixSalesItem>> GetItems(PretixOrganizer organizer, PretixEvent pretixEvent)
    {
        using var message = new HttpRequestMessage(HttpMethod.Get,
            new PretixEndpointBuilder()
                .WithOrganizer(organizer)
                .WithEvent(pretixEvent)
                .WithEndpoint("items").Build());

        var content = await ExecuteGetRequest(message);

        var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixSalesItem>>(content);
        
        foreach (var pretixSalesItem in deserialized.Results)
        {
            pretixSalesItem.Currency = pretixEvent.Currency;
        }

        return deserialized.Results;
    }

    public async Task<PretixOrderResponse> CreateOrder(
        PretixOrganizer organizer, 
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

        return deserialized;
    }

    public async Task<bool> CheckIn(PretixOrganizer organizer, PretixOrderResponse orderResponse)
    {
        var endpoint = new PretixEndpointBuilder().WithOrganizer(organizer)
                                                  .WithEndpoint("/checkinrpc/")
                                                  .WithEndpoint("/redeem/")
                                                  .Build();

        var json = PretixCheckinSerializer.SerializeCheckin(orderResponse);
        
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await ExecutePostRequest(endpoint, jsonContent);
        
        var deserialized = JsonSerializer.Deserialize<PretixStatus>(response);

        return deserialized?.Status == "ok";
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
}