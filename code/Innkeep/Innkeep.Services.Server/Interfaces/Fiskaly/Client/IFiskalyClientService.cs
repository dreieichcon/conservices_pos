using Innkeep.Api.Models.Fiskaly.Objects.Client;

namespace Innkeep.Services.Server.Interfaces.Fiskaly.Client;

public interface IFiskalyClientService
{
	public FiskalyClient? CurrentClient { get; set; }

	public IEnumerable<FiskalyClient> Clients { get; set; }

	public event EventHandler? ItemsUpdated;

	public Task Load();

	public Task<bool> CreateNew();

	public Task<bool> Activate();

	public Task<bool> Deactivate();

	public Task<bool> Save();
}