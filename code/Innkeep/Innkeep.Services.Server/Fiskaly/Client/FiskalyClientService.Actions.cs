using Innkeep.Api.Enum.Fiskaly.Client;

namespace Innkeep.Services.Server.Fiskaly.Client;

public partial class FiskalyClientService
{
	public async Task<bool> CreateNew()
	{
		if (IsTssNull())
			return false;

		var clientResult = await clientRepository.CreateClient(
			configService.CurrentItem!.TseId,
			Guid.NewGuid().ToString(),
			GenerateSerialNumber()
		);

		CurrentClient = clientResult.Object;

		ItemsUpdated?.Invoke(this, EventArgs.Empty);

		if (clientResult.IsSuccess)
			await Save();

		return clientResult.IsSuccess;
	}

	public async Task<bool> Deactivate()
	{
		if (IsTssNull() || CurrentClient is null)
			return false;

		var result = await clientRepository.UpdateClient(
			configService.CurrentItem!.TseId,
			CurrentClient.Id,
			ClientState.Deregistered
		);

		CurrentClient = result.Object;

		return result.IsSuccess;
	}

	public async Task<bool> Activate()
	{
		if (IsTssNull() || CurrentClient is null)
			return false;

		var result = await clientRepository.UpdateClient(
			configService.CurrentItem!.TseId,
			CurrentClient.Id,
			ClientState.Registered
		);

		CurrentClient = result.Object;

		return result.IsSuccess;
	}
}