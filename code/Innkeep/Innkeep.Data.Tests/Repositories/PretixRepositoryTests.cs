using System.Diagnostics;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Data.Tests.Repositories;

[TestClass]
public class PretixRepositoryTests
{
    [TestInitialize]
    public void TestInit()
    {
        DependencyManager.Initialize();
    }
    
    [TestMethod]
    public void GetOrganizers()
    {
        var repo = DependencyManager.ServiceProvider.GetRequiredService<IPretixRepository>();
        
        var response = Task.Run(() => repo.GetOrganizers()).Result;
        
        Trace.WriteLine(response);
    }

    [TestMethod]
    public void GetEvents()
    {
        var repo = DependencyManager.ServiceProvider.GetRequiredService<IPretixRepository>();
        
        var organizer = Task.Run(() => repo.GetOrganizers()).Result.First();

        var events = Task.Run(() => repo.GetEvents(organizer)).Result;

        foreach (var e in events)
        {
            Trace.WriteLine(e);
        }
    }
    
    [TestMethod]
    public void GetItems()
    {
        var repo = DependencyManager.ServiceProvider.GetRequiredService<IPretixRepository>();
        
        var organizer = Task.Run(() => repo.GetOrganizers()).Result.First();

        var events = Task.Run(() => repo.GetEvents(organizer)).Result;

        var items = Task.Run(() => repo.GetItems(organizer, events.First())).Result;

        foreach (var e in items)
        {
            Trace.WriteLine(e);
        }
    }
}