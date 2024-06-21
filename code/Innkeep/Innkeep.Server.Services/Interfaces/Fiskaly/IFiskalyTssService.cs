using Innkeep.Api.Models.Fiskaly.Objects;

namespace Innkeep.Server.Services.Interfaces.Fiskaly;

public interface IFiskalyTssService
{
	public IEnumerable<FiskalyTss> TssObjects { get; set; }

	public Task Load();
}