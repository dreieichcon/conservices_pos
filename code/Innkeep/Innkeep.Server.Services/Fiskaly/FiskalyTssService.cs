using Innkeep.Api.Fiskaly.Interfaces.Tss;
using Innkeep.Api.Models.Fiskaly.Objects;
using Innkeep.Server.Db.Models;
using Innkeep.Server.Services.Interfaces.Fiskaly;
using Innkeep.Services.Interfaces;

namespace Innkeep.Server.Services.Fiskaly;

public class FiskalyTssService(IDbService<FiskalyConfig> configService, IFiskalyTssRepository tssRepository) 
	: IFiskalyTssService
{
	public IEnumerable<FiskalyTss> TssObjects { get; set; } = new List<FiskalyTss>();

	public async Task Load()
	{
		TssObjects = await tssRepository.GetAll();
	}
}