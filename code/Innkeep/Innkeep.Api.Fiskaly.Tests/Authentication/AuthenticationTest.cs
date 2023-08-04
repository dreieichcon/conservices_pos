using System.Diagnostics;
using Innkeep.Api.Fiskaly.Interfaces;
using Innkeep.Core.Utilities;
using Innkeep.DI;
using Innkeep.Server.Services.Interfaces.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Innkeep.Api.Fiskaly.Tests.Authentication;

[TestClass]
public class AuthenticationTest
{
	[TestInitialize]
	public void TestInit()
	{
		DependencyManager.InitializeTests();
	}
	
	[TestMethod]
	public void TestAuthenticationRequest()
	{
		var repo = DependencyManager.ServiceProvider.GetRequiredService<IFiskalyAuthenticationRepository>();

		var result = Task.Run(() => repo.AuthenticateApi()).Result;
		
		if (result is not null)
			Trace.WriteLine(ClassDebugger.CreateDebugString(result));
	}
}