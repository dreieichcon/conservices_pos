﻿using Innkeep.Services.Client.Interfaces.Internal;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Innkeep.Client.Ui.Components.Layout;

public partial class MainLayout
{
	[Inject]
	public IEventRouter Router { get; set; } = null!;

	[Inject]
	public ISnackbar Snackbar { get; set; } = null!;

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Router.OnRegisterConnected += (_, _) => Snackbar.Add("Register Connected to Server", Severity.Success);
	}
}