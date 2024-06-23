using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Server.Db.Models;

namespace Innkeep.Api.Auth;

public interface IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; }
	
	public FiskalyTseConfig CurrentConfig { get; set; }

	public Task GetOrUpdateToken();

	public Task CreateTseConfig(FiskalyTss tss);

	public Task CreateTseConfig(string tseId);

	public Task<bool> SaveTseConfig();
	
}