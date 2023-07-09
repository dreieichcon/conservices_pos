using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Interfaces.Services;

namespace Innkeep.Server.Data.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
	public AuthenticationInfo Get()
	{
		using var db = InnkeepServerContext.Create();
		var authInfo = db.Authentications.Select(a => new AuthenticationInfo(a.Token)).FirstOrDefault();

		if (authInfo is not null) return authInfo;

		authInfo = new AuthenticationInfo(string.Empty);
		Create(authInfo);

		return authInfo;
	}

	private static void Create(AuthenticationInfo info)
	{
		using var db = InnkeepServerContext.Create();

		var create = new Authentication()
		{
			Token = info.PretixToken,
		};

		db.Authentications.Add(create);
		
		try
		{
			db.SaveChanges();
		}
		catch
		{
			// do nothing
		}
	}
	
	public bool Update(AuthenticationInfo info)
	{
		using var db = InnkeepServerContext.Create();

		var update = db.Authentications.First();

		update.Token = info.PretixToken;

		db.Authentications.Update(update);

		try
		{
			db.SaveChanges();
			return true;
		}
		catch
		{
			return false;
		}
		
	}
}