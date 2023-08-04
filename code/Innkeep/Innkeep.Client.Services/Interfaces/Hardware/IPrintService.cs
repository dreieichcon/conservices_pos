using Innkeep.Models.Printer;

namespace Innkeep.Client.Services.Interfaces.Hardware;

public interface IPrintService
{
	public void TestPage();

	void Print(Receipt result);
}