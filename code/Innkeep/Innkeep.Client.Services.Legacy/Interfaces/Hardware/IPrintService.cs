using Innkeep.Models.Printer;

namespace Innkeep.Client.Services.Legacy.Interfaces.Hardware;

public interface IPrintService
{
	public Receipt? LastReceipt { get; set; }
	
	public void TestPage();

	void Print();

	void Drawer();

	public void PrintImage(string path);
}