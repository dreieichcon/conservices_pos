using Innkeep.Api.Models.Fiskaly.Objects;

namespace Innkeep.Server.Services.Interfaces.Fiskaly;

public interface IFiskalyTssService
{
	public event EventHandler? ItemsUpdated;
	
	public IEnumerable<FiskalyTss> TssObjects { get; set; }
	
	public FiskalyTss? CurrentTss { get; set; }

	public Task Load();

	public Task<bool> Save();

	public Task<bool> CreateNew();

	public Task<bool> Deploy();

	public Task<bool> ChangeAdminPin();

	public Task<bool> InitializeTss();
	
}