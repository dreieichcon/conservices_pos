using Innkeep.Client.Services.Legacy.Interfaces.Ui;

namespace Innkeep.Client.Services.Legacy.Services.Ui;

public class PopupService : IPopupService
{
	public event EventHandler? PopupChanged;

	public void Show(string popupName)
	{
		PopupChanged?.Invoke(popupName, new PopupEventArgs()
		{
			Hide = false
		});
	}

	public void Close(string popupName)
	{
		PopupChanged?.Invoke(popupName, new PopupEventArgs());
	}
}

public class PopupEventArgs : EventArgs
{
	public bool Hide { get; init; } = true;
}