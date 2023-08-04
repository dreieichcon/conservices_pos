using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Interfaces;
using Innkeep.Server.Services.Models;

namespace Innkeep.Server.Services.Services;

public class TseService : ITseService
{
	public async Task<TseResult> CreateEntry(PretixTransaction pretixTransaction)
	{
		return new TseResult()
		{
			Checksum = "checksum",
			EndTime = DateTime.Now,
			Signature = "signature",
			TseTransactionNumber = $"{Guid.NewGuid().ToString().Replace("-", "")}"
		};
	}
}