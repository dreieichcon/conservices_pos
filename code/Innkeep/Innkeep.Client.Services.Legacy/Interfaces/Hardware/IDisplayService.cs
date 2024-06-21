namespace Innkeep.Client.Services.Legacy.Interfaces.Hardware;

public interface IDisplayService
{
	public void TestDisplay();

	public void ShowText(string text);

	public void ClearText();
}