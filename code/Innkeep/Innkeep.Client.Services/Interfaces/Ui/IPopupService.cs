namespace Innkeep.Client.Services.Interfaces.Ui;

public interface IPopupService
{
	public event EventHandler PopupChanged;

	public void Show(string popupName);

	public void Close(string popupName);
}