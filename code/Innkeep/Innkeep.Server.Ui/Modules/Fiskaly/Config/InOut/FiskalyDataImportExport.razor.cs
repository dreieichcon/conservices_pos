using System.Text.Json;
using Innkeep.Api.Auth;
using Innkeep.Api.Json;
using Innkeep.Core.Cryptography;
using Innkeep.Core.FileIo;
using Innkeep.Db.Server.Models.Server;
using Innkeep.Server.Ui.Modules.Fiskaly.Config.InOut.Components;
using Innkeep.Services.Server.Interfaces.Fiskaly.Tss;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Server.Ui.Modules.Fiskaly.Config.InOut;

public partial class FiskalyDataImportExport : ComponentBase
{
	[Inject]
	public IFiskalyAuthenticationService AuthenticationService { get; set; } = null!;

	[Inject]
	public IFiskalyTssService TssService { get; set; } = null!;

	[Inject]
	public IDialogService DialogService { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

	private IEnumerable<FiskalyTseConfig> Configs { get; set; } = [];

	public FiskalyTseConfig? CurrentConfig { get; set; }

	private async Task Reload()
	{
		Configs = await AuthenticationService.GetAll();
	}

	private string GetTssName(FiskalyTseConfig? config)
	{
		if (config is null)
			return string.Empty;

		var item = TssService.TssObjects.FirstOrDefault(x => x.Id == config.TseId);

		return $"{item?.Description} ({item?.State})";
	}

	private async Task Export()
	{
		if (CurrentConfig is null)
			return;

		var dialog = await DialogService.ShowAsync<FiskalyDataExportDialog>();
		var result = await dialog.Result;

		if (result is null || result.Canceled)
			return;

		var password = result.Data!.ToString();

		var data = JsonSerializer.Serialize(CurrentConfig, SerializerOptions.GetServerOptions());

		var encrypted = await EncryptionHelper.EncryptAsync(data, password!);

		var outputFolder = PathHelper.GetDesktopExportPath();

		var outputPath = PathHelper.GetTimestampExportPath(outputFolder, $"{CurrentConfig.TseId}.tss");

		await File.WriteAllBytesAsync(outputPath, encrypted);

		Snackbar.Add("Exported File", Severity.Success);
	}

	private async Task Import()
	{
		var dialog = await DialogService.ShowAsync<FiskalyDataImportDialog>();
		var result = await dialog.Result;

		if (result is null || result.Canceled)
			return;

		var (fileContent, password) = (Tuple<byte[], string>) result.Data!;

		var decrypted = await EncryptionHelper.DecryptAsync(fileContent, password);

		FiskalyTseConfig? deserialized;

		try
		{
			deserialized = JsonSerializer.Deserialize<FiskalyTseConfig>(
				decrypted,
				SerializerOptions.GetServerOptions()
			);
		}
		catch (Exception e)
		{
			Snackbar.Add("Incorrect Password or File Format", Severity.Error);

			return;
		}

		if (deserialized is null)
			return;

		if (Configs.Any(x => x.Id == deserialized.Id || x.TseId == deserialized.TseId))
		{
			var overwriteResult = await DialogService.ShowMessageBox(
				"Warning",
				"A config for this TSS already exists. Do you want to overwrite it?",
				"Ok",
				"Cancel"
			);

			if (overwriteResult is true)
			{
				await ImportConfig(deserialized);

				return;
			}
		}

		await ImportConfig(deserialized);
	}

	private async Task ImportConfig(FiskalyTseConfig? config)
	{
		var importResult = await AuthenticationService.Import(config);

		if (importResult)
			Snackbar.Add("Import Successful", Severity.Success);
		else
			Snackbar.Add("Import Failed", Severity.Error);
	}
}