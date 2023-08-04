using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Client.Services.Interfaces.Server;

namespace Innkeep.Client.Services.Services.Pretix;

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
}