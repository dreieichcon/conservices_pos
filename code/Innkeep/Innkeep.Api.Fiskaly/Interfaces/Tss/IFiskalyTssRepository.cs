using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Fiskaly.Interfaces.Tss;

public interface IFiskalyTssRepository
{
	public Task<IHttpResponse<IEnumerable<FiskalyTss>>> GetAll();

	public Task<FiskalyTss> GetOne(string id);

	public Task<IHttpResponse<FiskalyTss>> CreateTss(string id);

	public Task<IHttpResponse<FiskalyTss>> DeployTss(FiskalyTss current);

	public Task<IHttpResponse<bool>> ChangeAdminPin(FiskalyTss current);

	public Task<IHttpResponse<FiskalyTss>> InitializeTss(FiskalyTss current);
}