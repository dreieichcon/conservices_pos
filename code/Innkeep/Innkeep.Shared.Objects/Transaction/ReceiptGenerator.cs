using System.Globalization;
using Innkeep.Core.DomainModels.KassSichV;
using Innkeep.Core.DomainModels.Print;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Shared.Objects.Transaction;

public static class ReceiptGenerator
{
	public static Receipt Generate(TransactionServiceResult result)
	{
		var receipt = new Receipt().AddTitle(result.EventName)
							.AddLines(result.OrganizerInfo.Split(Environment.NewLine))
							.AddBlank()
							.AddDivider();

		foreach (var item in result.Transaction.TransactionItems)
		{
			receipt.AddLine(item.LineInfo("eur"))
					.AddLine(item.LinePricing("eur"))
					.AddBlank();
		}

		receipt.AddBlank().AddDivider();

		var sum = $"Summe:      {result.Transaction.Sum.ToString().PadLeft(6, ' ')}€";
		var bck = $"Rück:       {result.Transaction.Return.ToString().PadLeft(6, ' ').Replace("-", "")}€";
		
		var giv = $"Erhalten:   {$"{result.Transaction.AmountGiven:0.##}".PadLeft(6, ' ')}€";
		
		
		receipt.AddSum(sum.PadLeft(42, ' '))
				.AddSum(giv.PadLeft(42, ' '))
				.AddSum(bck.PadLeft(42, ' '))
				.AddBlank()
				.AddDivider()
				.AddCenteredLine($"Order ID: {result.OrderResponse.Code}")
				.AddCenteredLine($"Order GUID: {result.Guid}")
				.AddBlank()
				.AddDivider();

		receipt.AddLine($"TSE Transaktionsnummer: {result.TseResult.TseTransactionNumber}")
				.AddLine($"Seriennr. Kasse: {result.RegisterId}")
				.AddLine($"Prüfwert: {result.TseResult.Checksum}")
				.AddLine($"Signaturzähler: {result.TseResult.Signature}")
				.AddLine($"Startzeit: {result.TseResult.StartTime}")
				.AddLine($"Endzeit: {result.TseResult.EndTime}")
				.AddBlank()
				.AddDivider();
		
		receipt.AddLine("Datum / Uhrzeit:")
				.AddLine(DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("de")));
				
		return receipt;
	}
}