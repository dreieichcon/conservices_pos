using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Server.Db.Models;

namespace Innkeep.Api.Fiskaly.Interfaces.Tss;

public interface IFiskalyTssRepository
{
	public Task<IEnumerable<FiskalyTss>> GetAll();

	public Task<FiskalyTss> GetOne(string id);

	public Task<FiskalyTss?> CreateTss(string id);

	public Task<FiskalyTss?> DeployTss(FiskalyTss current);

	public Task<bool> ChangeAdminPin(FiskalyTss current);

	public Task<FiskalyTss?> InitializeTss(FiskalyTss current);
}