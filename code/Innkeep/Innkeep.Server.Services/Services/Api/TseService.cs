using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Interfaces.Api;
using Innkeep.Server.Services.Models;

namespace Innkeep.Server.Services.Services.Api;

public class TseService : ITseService
{
	private readonly IFiskalyService _fiskalyService;

	public TseService(IFiskalyService fiskalyService)
	{
		_fiskalyService = fiskalyService;
	}
	
	public async Task<TseResult?> CreateEntry(PretixTransaction transaction)
	{
		return await _fiskalyService.CreateTransaction(transaction);
	}
}