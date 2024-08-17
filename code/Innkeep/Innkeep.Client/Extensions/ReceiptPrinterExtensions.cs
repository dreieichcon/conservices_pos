using DemoPOS.Document;
using ESCPOS;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;

namespace Innkeep.Client.Extensions;

public static class ReceiptPrinterExtensions
{
	private const int MaxLength = 42;

	public static DocumentManager AddSeparatedLines(this DocumentManager manager, string input)
	{
		foreach (var line in input.Split(Environment.NewLine))
		{
			manager.AddLine(line, Justification.Center);
		}

		return manager;
	}

	public static DocumentManager AddDashedLine(this DocumentManager manager) => manager.AddLine(new string('-', 42));

	public static DocumentManager AddReceiptLines(this DocumentManager manager, TransactionReceipt receipt)
	{
		if (receipt.Lines.Exists(x => x.HasTax))
			manager.AddLine(SpaceEvenlyAcross("Stk.", "Artikel  ", "MwSt.", receipt.Currency));

		else
			manager.AddLine(SpaceEvenlyAcross("Stk.", "Artikel  ", receipt.Currency));

		manager.AddDashedLine();

		foreach (var line in receipt.Lines)
		{
			manager.AddLine(CreateReceiptLine(line));
		}

		manager.AddEmptyLine();

		return manager;
	}

	public static DocumentManager AddReceiptSum(this DocumentManager manager, TransactionReceipt receipt)
	{
		manager.AddDashedLine();
		manager.AddLine(SpaceEvenlyAcross("Gesamt", receipt.Sum.TotalAmountString));

		manager.AddEmptyLine();

		manager.AddLine(SpaceEvenlyAcross("Gegeben", receipt.Sum.AmountGivenString));
		manager.AddLine(SpaceEvenlyAcross("Zurück", receipt.Sum.AmountReturnedString));
		
		return manager;
	}

	public static DocumentManager AddReceiptTaxIfExists(this DocumentManager manager, TransactionReceipt receipt)
	{
		if (!receipt.Lines.Exists(x => x.HasTax)) return manager;
		
		manager.AddDashedLine();
		manager.AddLine(SpaceEvenlyAcross("Satz", "Netto ", "MwSt  ", "Brutto"));

		foreach (var tax in receipt.TaxInformation)
		{
			manager.AddLine(SpaceEvenlyAcross(tax.NameString, tax.NetString, tax.TaxAmountString, tax.GrossString));
		}

		return manager;
	}

	public static DocumentManager AddTransactionInfo(this DocumentManager manager, TransactionReceipt receipt)
	{
		manager.AddLine(SpaceEvenlyAcross("Transaktion:", receipt.TransactionCounter.ToString()));
		
		if (!string.IsNullOrEmpty(receipt.TransactionId))
			manager.AddLine(SpaceEvenlyAcross("Id:", receipt.TransactionId));
		
		return manager;
	}

	private static string SpaceEvenlyAcross(params string[] strings)
	{
		var totalLength = strings.Sum(x => x.Length);

		var spacing = (int) Math.Floor((decimal) (MaxLength - totalLength) / (strings.Length - 1));
		var spacer = new string(' ', spacing);

		return string.Join(spacer, strings);
	}

	private static string CreateReceiptLine(ReceiptLine line)
	{
		// 8 (20) 4 10
		return $"{line.CountString}{line.NameString}{line.TaxRateString}{line.PriceString}";
	}
}