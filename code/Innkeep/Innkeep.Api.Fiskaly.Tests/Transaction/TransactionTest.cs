using System.Diagnostics;
using Innkeep.Api.Pretix.Models.Base;
using Innkeep.Api.Pretix.Models.Internal;
using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.DI;
using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Interfaces.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Api.Fiskaly.Tests.Transaction;

[TestClass]
public class TransactionTest
{
	[TestInitialize]
	public void TestInit()
	{
		DependencyManager.InitializeTests();
	}

	[TestMethod]
	public void CreateTransaction()
	{
		var tseService = DependencyManager.ServiceProvider.GetRequiredService<ITseService>();

		var transaction = new PretixTransaction()
		{
			TransactionId = Guid.NewGuid(),
			TransactionStart = DateTime.Now,
			TransactionItems = new List<PretixCartItem<PretixSalesItem>>()
			{
				new (
					new PretixSalesItem()
					{
						Name = new MultiString()
						{
							GermanDefault = "Test"
						},
						Currency = "EUR",
						DefaultPrice = 10,
						SalesChannels = new []{"POS"}
					}
				)
				{
					Count = 5
				},
			}
		};

		var result = Task.Run(() => tseService.CreateEntry(transaction)).Result;
        
		Trace.WriteLine(result);
	}
}