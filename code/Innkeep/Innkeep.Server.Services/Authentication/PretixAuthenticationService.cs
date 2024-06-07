using Innkeep.Api.Auth;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Authentication;

public class PretixAuthenticationService : IPretixAuthenticationService
{
	private readonly IDbService<PretixConfig> _pretixService;

	public PretixAuthenticationService(IDbService<PretixConfig> pretixService)
	{
		_pretixService = pretixService;
		_pretixService.ItemsUpdated += (_, _) => Load();
	}
	
	public AuthenticationInfo AuthenticationInfo { get; set; } = new(string.Empty);

	public void Load()
	{
		var token = _pretixService.CurrentItem.PretixAccessToken ?? string.Empty;
		AuthenticationInfo = new AuthenticationInfo(token);
	}
}