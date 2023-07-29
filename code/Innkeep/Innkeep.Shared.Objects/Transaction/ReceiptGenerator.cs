using System.Globalization;
using Innkeep.Core.DomainModels.KassSichV;
using Innkeep.Core.DomainModels.Print;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Shared.Objects.Transaction;

public static class ReceiptGenerator
{
	public static Receipt Generate
		(Transaction transaction, PretixOrderResponse pretixResult, TseResult tseResult, string? organizerInfo, string eventName)
	{
		var receipt = new Receipt().AddTitle(eventName)
							.AddLines(organizerInfo.Split(Environment.NewLine))
							.AddBlank()
							.AddDivider();

		foreach (var item in transaction.TransactionItems)
		{
			receipt.AddLine(item.LineInfo("eur")).AddBlank();
		}

		receipt.AddBlank().AddDivider();

		receipt.AddSum($"Summe: {transaction.Sum.ToString().PadLeft(5, ' ')}€")
				.AddSum($"Erhalten: {transaction.AmountGiven.ToString().PadLeft(5, ' ')}€")
				.AddSum($"Rück: {transaction.Return.ToString().PadLeft(5, ' ')}€")
				.AddBlank()
				.AddDivider()
				.AddCenteredLine($"Order ID: {pretixResult.Code}")
				.AddLine("Datum / Uhrzeit:")
				.AddLine(DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("de")))
				.AddBlank()
				.AddDivider();

		return receipt;
	}
}