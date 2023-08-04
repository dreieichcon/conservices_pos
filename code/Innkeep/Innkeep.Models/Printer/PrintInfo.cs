using System.Text.Json.Serialization;

namespace Innkeep.Models.Printer;

public class PrintInfo
{
	public PrintInfo(string content, LineType lineType)
	{
		Content = content;
		LineType = lineType;
	}

	[JsonPropertyName("content")]
	public string Content { get; init; }

	[JsonPropertyName("linetype")]
	public LineType LineType { get; init; }

}