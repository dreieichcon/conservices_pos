using Innkeep.Client.Interfaces.Services;
using Innkeep.Core.Interfaces.Services;

namespace Innkeep.DI.Services;

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