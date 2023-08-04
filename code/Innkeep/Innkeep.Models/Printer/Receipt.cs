using System.Text.Json.Serialization;
using Innkeep.Core.DomainModels.Print;

namespace Innkeep.Models.Printer;

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

	public Receipt AddTitle(string content)
	{
		Lines.Add(new PrintInfo(content, LineType.Title));
		return this;
	}
	
	public Receipt AddLine(string content)
	{
		Lines.Add(new PrintInfo(content, LineType.Line));
		return this;
	}

	public Receipt AddLines(string[] lines)
	{
		foreach (var line in lines)
		{
			Lines.Add(new PrintInfo(line, LineType.Line));
		}

		return this;
	}

	public Receipt AddBlank()
	{
		Lines.Add(new PrintInfo("", LineType.Blank));
		return this;
	}

	public Receipt AddDivider()
	{
		Lines.Add(new PrintInfo("", LineType.Divider));
		return this;
	}

	public Receipt AddSum(string content)
	{
		Lines.Add(new PrintInfo(content, LineType.Sum));

		return this;
	}

	public Receipt AddCenteredLine(string content)
	{
		Lines.Add(new PrintInfo(content, LineType.Center));
		return this;
	}
}