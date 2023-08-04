using ESCPOS;
using ESCPOS.Utils;
using static ESCPOS.Commands;
using System.Drawing;
using Innkeep.Printer.Util;

namespace Innkeep.Printer.Document;

public class DocumentManager
{
	private byte[] _commands = Array.Empty<byte>();
	private readonly string _port;
	
	public DocumentManager(string port)
	{
		_port = port;
		PageSetup();
	}

	public DocumentManager AddTitle(string title, 
				Justification justification = Justification.Center,
				CharSizeWidth width = CharSizeWidth.Double,
				CharSizeHeight height = CharSizeHeight.Double
				)
	{
		Append(SelectJustification(justification));
		Append(SelectCharSize(width, height));
		Append(title.GetBytes());
		ResetFontAndJustification();
		Append(LF);

		return this;
	}

	public DocumentManager AddLine(string lineText, 
		Justification justification = Justification.Left)
	{
		Append(lineText.GetBytes());
		Append(LF);
		ResetJustification();
		return this;
	}

	public DocumentManager AddEmptyLine()
	{
		Append(LF);
		return this;
	}

	public DocumentManager AddQrCode(string content)
	{
		Append(SelectJustification(Justification.Center));
		Append(PrintQRCode(content));
		Append(LF);

		return this;
	}

	public DocumentManager AddImage(string path)
	{
		Append(CreateImageArray(new Bitmap(path)));
		Append(LF);

		return this;
	}

	public DocumentManager Cut()
	{
		Append(LF, 5);
		Append(FullPaperCut);
		return this;
	}

	public DocumentManager PrintAndContinue()
	{
		_commands.Print(_port);
		return this;
	}

	public void Print()
	{
		_commands.Print(_port);
		_commands = Array.Empty<byte>();
	}

	private void ResetFontAndJustification()
	{
		ResetFont();
		ResetJustification();
	}

	private void ResetFont()
	{
		Append(SelectCharSize(CharSizeWidth.Normal, CharSizeHeight.Normal));
	}

	private void ResetJustification()
	{
		Append(SelectJustification(Justification.Left));
	}

	private byte[] CreateImageArray(Bitmap image)
	{
		var width = image.Width;
		var height = image.Height;

		if (width > 384)
		{
			var sizeDivider = (int)Math.Ceiling(image.Width / 384d);
			image = new Bitmap(image, new Size(width / sizeDivider, height / sizeDivider));
		}
		
		var bytes = new Image.Image();

		var byteArray = bytes.Print(image);

		return byteArray;
	}

	private void PageSetup()
	{
		Append(SelectCodeTable(CodeTable.Windows1252));
	}

	private void Append(byte[] command, int count = 1)
	{
		for (var i = 0; i < count; i++)
		{
			_commands = _commands.Add(command);
		}
	}
}