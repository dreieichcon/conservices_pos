using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Innkeep.Core.DomainModels.Pretix;
using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.Data.Connectors;
using RestSharp;

namespace Innkeep.Data.Repositories.Pretix;

public class PretixRepository : IPretixRepository
{
    private readonly HttpClient _pretixClient;

    private readonly IAuthenticationService _authenticationService;

    private string Token => _authenticationService.AuthenticationInfo.PretixToken;
    
    public PretixRepository(IAuthenticationService authenticationService)
    {
     Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
     
     _authenticationService = authenticationService;
        _pretixClient = new HttpClient();
    }

    public async Task<string?> TestConnection()
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, new PretixEndpointBuilder().WithEndpoint("organizers").Build());
        
        PrepareHeaders(message);

        var response = await _pretixClient.SendAsync(message);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<IEnumerable<PretixOrganizer>> GetOrganizers()
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, 
            new PretixEndpointBuilder().WithEndpoint("organizers").Build());
        
        var content = await ExecuteRequest(message);

        var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixOrganizer>>(content);

        return deserialized!.Results;
    }

    public async Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer)
    {
        using var message = new HttpRequestMessage(HttpMethod.Get,
            new PretixEndpointBuilder()
                .WithOrganizer(organizer)
                .WithEndpoint("events").Build());

        var content = await ExecuteRequest(message);
            
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

        var content = await ExecuteRequest(message);

        var deserialized = JsonSerializer.Deserialize<PretixResponse<PretixSalesItem>>(content);
        
        foreach (var pretixSalesItem in deserialized.Results)
        {
            pretixSalesItem.Currency = pretixEvent.Currency;
        }

        return deserialized.Results;
    }

    private async Task<string> ExecuteRequest(HttpRequestMessage message)
    {
        PrepareHeaders(message);
        var response = await _pretixClient.SendAsync(message);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            throw new HttpRequestException(response.StatusCode.ToString());
        }
    }

    private void PrepareHeaders(HttpRequestMessage message)
    {
        message.Headers.Add("Accept", "application/json");
        message.Headers.Authorization = new AuthenticationHeaderValue("Token", Token);
    }

    private void ConnectionLog(HttpRequestMessage message)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Method: {message.Method}");
        
        sb.AppendLine($"Destinaton: {message.RequestUri}");
        
        sb.AppendLine("Headers:");
        
        foreach (var parameter in message.Headers)
        {
            sb.AppendLine($"{parameter.Key}: {parameter.Value.First()}");
        }
        
        Trace.WriteLine(sb.ToString());
    }

    private void RequestLog(RestResponse response)
    {
        var sb = new StringBuilder();

        sb.AppendLine(new string('-', 50));

        foreach (var header in response.Headers)
        {
            sb.AppendLine($"{header.Name}: {header.Value}");
        }

        Trace.WriteLine(sb.ToString());
    }
}