using Innkeep.Api.Models.Fiskaly.Objects;

namespace Innkeep.Server.Services.Interfaces.Fiskaly;

public interface IFiskalyTssService
{
	public IEnumerable<FiskalyTss> TssObjects { get; set; }
	
	public FiskalyTss? CurrentTss { get; set; }

	public Task Load();

	public Task<bool> Save();
}