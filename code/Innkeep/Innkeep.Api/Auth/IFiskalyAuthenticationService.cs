using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Core.DomainModels.Authentication;
using Innkeep.Db.Server.Models.Server;

namespace Innkeep.Api.Auth;

public interface IFiskalyAuthenticationService
{
	public AuthenticationInfo AuthenticationInfo { get; set; }
	
	public FiskalyTseConfig CurrentConfig { get; set; }

	public Task GetOrUpdateToken();

	public Task CreateTseConfig(FiskalyTss tss);

	public Task CreateTseConfig(string tseId);

	public Task<bool> SaveTseConfig();

	public Task<IEnumerable<FiskalyTseConfig>> GetAll();

	public Task<bool> Import(FiskalyTseConfig? imported);
}