using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Client.Services.Legacy.Interfaces.Server;

namespace Innkeep.Client.Services.Legacy.Services.Pretix;

public class ClientPretixRepository : IClientPretixRepository
{
	private readonly IClientServerConnectionRepository _clientServerConnectionRepository;
	public ClientPretixRepository(IClientServerConnectionRepository clientServerConnectionRepository)
	{
		_clientServerConnectionRepository = clientServerConnectionRepository;
	}

	public async Task<PretixOrganizer> GetOrganizer()
	{
		return await _clientServerConnectionRepository.GetOrganizer();
	}

	public async Task<PretixEvent> GetEvent()
	{
		return await _clientServerConnectionRepository.GetEvent();
	}
	public async Task<IEnumerable<PretixSalesItem>> GetSalesItems()
	{
		return await _clientServerConnectionRepository.GetSalesItems();
	}

	public async Task<IEnumerable<PretixCheckinList>> GetCheckinLists()
	{
		return await _clientServerConnectionRepository.GetCheckinLists();
	}
}