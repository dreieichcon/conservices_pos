using Innkeep.Core.DomainModels.Print;

namespace Innkeep.Client.Interfaces.Services;

public interface IPrintService
{
	public void TestPage();

	void Print(Receipt result);
}