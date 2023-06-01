namespace Innkeep.Core.Interfaces.Services;

public interface IPopupService
{
	public event EventHandler PopupChanged;

	public void Show(string popupName);

	public void Close(string popupName);
}