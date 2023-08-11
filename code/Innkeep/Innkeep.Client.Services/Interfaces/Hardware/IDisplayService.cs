namespace Innkeep.Client.Services.Interfaces.Hardware;

public interface IDisplayService
{
	public void TestDisplay();

	public void ShowText(string text);

	public void ClearText();
}