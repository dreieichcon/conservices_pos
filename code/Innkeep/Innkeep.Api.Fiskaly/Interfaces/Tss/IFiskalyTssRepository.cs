using Innkeep.Api.Models.Fiskaly.Objects;

namespace Innkeep.Api.Fiskaly.Interfaces.Tss;

public interface IFiskalyTssRepository
{
	public Task<IEnumerable<FiskalyTss>> GetAll();

	public Task<FiskalyTss> GetOne(string id);
}