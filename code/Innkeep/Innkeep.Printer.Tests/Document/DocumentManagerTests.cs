using Innkeep.Printer.Document;

namespace Innkeep.Printer.Tests.Document;

[TestClass]
public class DocumentManagerTests
{
	public void Setup()
	{
		
	}

	[TestMethod]
	public void TestPrint()
	{
		var doc = new DocumentManager("COM3");
		doc.AddImage(@"C:\Users\TB-LocalTemp\Downloads\gnome.bmp")
			.Cut()
			.Print();
	}
}