using System.Text.Json.Serialization;

namespace Innkeep.Core.DomainModels.Print;

public class Receipt
{
	public Receipt(IList<PrintInfo> lines)
	{
		Lines = lines;
	}

	public Receipt()
	{
		Lines = new List<PrintInfo>();
	}

	[JsonPropertyName("lines")]
	public IList<PrintInfo> Lines { get; init; }
}