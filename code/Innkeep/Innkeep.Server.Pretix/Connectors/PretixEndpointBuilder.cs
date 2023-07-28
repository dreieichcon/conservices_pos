using Innkeep.Core.Core;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Server.Pretix.Connectors;

public class PretixEndpointBuilder : BaseEndpointBuilder
{
    public PretixEndpointBuilder() : base("https://pretix.eu/api/v1/")
    {
        
    }

    public PretixEndpointBuilder WithOrganizer(PretixOrganizer organizer)
    {
        Endpoints.Add("organizers");
        Endpoints.Add(organizer.Slug);
        return this;
    }

    public PretixEndpointBuilder WithEvent(PretixEvent evt)
    {
        Endpoints.Add("events");
        Endpoints.Add(evt.Slug);
        return this;
    }
}