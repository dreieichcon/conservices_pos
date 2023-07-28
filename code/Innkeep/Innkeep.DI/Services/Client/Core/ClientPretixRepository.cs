using Innkeep.Client.Interfaces.Services;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.DI.Services.Client.Core;

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