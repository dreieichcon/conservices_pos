using Innkeep.Api.Auth;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Interfaces;
using Innkeep.Server.Db.Models;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Authentication;

public class PretixAuthenticationService(IDbService<PretixConfig> pretixService) : IPretixAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; } = new(string.Empty);

	public async Task Load()
	{
		await pretixService.Load();
		var token = pretixService.CurrentItem.PretixAccessToken ?? string.Empty;
		AuthenticationInfo = new AuthenticationInfo(token);
	}
}