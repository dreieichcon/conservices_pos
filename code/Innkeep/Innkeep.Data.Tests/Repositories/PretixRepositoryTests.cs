using System.Diagnostics;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Data.Tests.Repositories;

[TestClass]
public class PretixRepositoryTests
{
    [TestInitialize]
    public void TestInit()
    {
        DependencyManager.InitializeTests();
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

    [TestMethod]
    public void CreateTestTransaction()
    {
        var repo = DependencyManager.ServiceProvider.GetRequiredService<IPretixRepository>();
        
        var organizer = Task.Run(() => repo.GetOrganizers()).Result.First();

        var events = Task.Run(() => repo.GetEvents(organizer)).Result;
        
        var ev = events.First();

        var items = Task.Run(() => repo.GetItems(organizer, ev)).Result;

        var item = items.First(x => x.DefaultPrice > 0);

        var cart = new List<PretixCartItem<PretixSalesItem>>()
        {
            new (item)
            {
                Count = 5
            }
        };

        var transaction = Task.Run(() => repo.CreateOrder(organizer, ev, cart)).Result;
        
        Trace.WriteLine(transaction);
    }

    [TestMethod]
    public void CreateTestTransactionAndCheckIn()
    {
        var repo = DependencyManager.ServiceProvider.GetRequiredService<IPretixRepository>();
        
        var organizer = Task.Run(() => repo.GetOrganizers()).Result.First();

        var events = Task.Run(() => repo.GetEvents(organizer)).Result;
        
        var ev = events.First();

        var items = Task.Run(() => repo.GetItems(organizer, ev)).Result;

        var item = items.First(x => x.DefaultPrice > 0);

        var cart = new List<PretixCartItem<PretixSalesItem>>()
        {
            new PretixCartItem<PretixSalesItem>(item)
            {
                Count = 1
            }
        };

        var transactionResult = Task.Run(() => repo.CreateOrder(organizer, ev, cart)).Result;

        var checkInStatus = Task.Run(() => repo.CheckIn(organizer, transactionResult)).Result;
        
        Trace.WriteLine($"Checked In: {checkInStatus}");
    }
}