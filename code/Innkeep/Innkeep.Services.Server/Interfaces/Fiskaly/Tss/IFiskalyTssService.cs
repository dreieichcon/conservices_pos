using Innkeep.Api.Models.Fiskaly.Objects.Tss;

namespace Innkeep.Services.Server.Interfaces.Fiskaly.Tss;

public interface IFiskalyTssService
{
	public IEnumerable<FiskalyTss> TssObjects { get; set; }

	public FiskalyTss? CurrentTss { get; set; }

	public event EventHandler? ItemsUpdated;

	public Task Load();

	public Task<bool> Save();

	public Task<bool> CreateNew();

	public Task<bool> Deploy();

	public Task<bool> ChangeAdminPin();

	public Task<bool> InitializeTss();
}