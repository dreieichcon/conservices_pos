namespace Innkeep.Services.Server.Fiskaly.Tss;

public partial class FiskalyTssService
{
	public async Task<bool> CreateNew()
	{
		var tssResult = await tssRepository.CreateTss(Guid.NewGuid().ToString());

		if (!tssResult.IsSuccess) return false;

		CurrentTss = tssResult.Object!;

		await authenticationService.CreateTseConfig(CurrentTss);

		await Save();

		return authenticationService.CurrentConfig.TseId == CurrentTss.Id &&
				!string.IsNullOrEmpty(authenticationService.CurrentConfig.TsePuk);
	}

	public async Task<bool> Deploy()
	{
		var result = await tssRepository.DeployTss(CurrentTss!);

		return result.IsSuccess;
	}

	public async Task<bool> ChangeAdminPin()
	{
		var result = await tssRepository.ChangeAdminPin(CurrentTss!);

		if (result.IsSuccess)
			await Save();

		return result.IsSuccess;
	}

	public async Task<bool> InitializeTss()
	{
		var result = await tssRepository.InitializeTss(CurrentTss!);

		if (!result.IsSuccess)
			return false;

		CurrentTss = result.Object;
		await Save();

		return true;
	}
}