using Innkeep.Core.DomainModels.Pretix;

namespace Innkeep.Data.Connectors;

public class PretixEndpointBuilder
{
    private const string Endpoint = "https://pretix.eu/api/v1/";

    private readonly List<string> _endpoints = new();
    
    
    public PretixEndpointBuilder()
    {
        
    }

    public PretixEndpointBuilder WithEndpoint(string endpoint)
    {
        _endpoints.Add(endpoint.Replace("/", ""));
        return this;
    }

    public PretixEndpointBuilder WithOrganizer(PretixOrganizer organizer)
    {
        _endpoints.Add("organizers");
        _endpoints.Add(organizer.Slug);
        return this;
    }

    public PretixEndpointBuilder WithEvent(PretixEvent evt)
    {
        _endpoints.Add("events");
        _endpoints.Add(evt.Slug);
        return this;
    }

    public string Build()
    {
        return Endpoint + string.Join("/", _endpoints) + "/";
    }
}