using System.Globalization;
using Innkeep.Models.Printer;
using Innkeep.Server.Services.Legacy.Models;

namespace Innkeep.Server.Services.Legacy.Util;

public static class ReceiptGenerator
{
	public static Receipt Generate(TransactionServiceResult result)
	{
		var receipt = new Receipt().AddTitle(result.EventName);
		
		if (result.OrganizerInfo != null) 
			receipt.AddLines(result.OrganizerInfo.Split(Environment.NewLine));
		
		receipt.AddBlank().AddDivider();

		foreach (var item in result.PretixTransaction.TransactionItems)
		{
			receipt.AddLine(item.LineInfo("eur"))
					.AddLine(item.LinePricing("eur"))
					.AddBlank();
		}

		receipt.AddDivider();

		var sum = $"{"Summe:",-20}{result.PretixTransaction.Sum.ToString(CultureInfo.InvariantCulture),6}€";
		var bck = $"{"Rück:",-20}{result.PretixTransaction.Return.ToString(CultureInfo.InvariantCulture).PadLeft(6, ' ').Replace("-", " ")}€";
		var giv = $"{"Erhalten:",-20}{$"{result.PretixTransaction.AmountGiven:0.00}",6}€";
		
		
		receipt.AddSum(sum.PadLeft(42, ' '))
				.AddSum(giv.PadLeft(42, ' '))
				.AddSum(bck.PadLeft(42, ' '))
				.AddDivider()
				.AddCenteredLine($"Order ID: {result.OrderResponse.Code}")
				.AddCenteredLine($"Order GUID: {result.Guid}")
				.AddDivider();

		if (result.TseResult is not null)
		{
			/// TSE DATA
			receipt.AddJustifiedLine("TSE-Anbieter:", "fiskaly GmbH")
					.AddLine($"TSE-Signatur: {result.TseResult.Signature}")
					.AddJustifiedLine("TSE-Erstbestellung:",$"{result.PretixTransaction.TransactionStart}")
					.AddJustifiedLine("TSE-Transaktionsnummer:",$"{result.TseResult.TseTransactionNumber}")
					.AddJustifiedLine("TSE-Start:", $"{result.TseResult.StartTime}")
					.AddJustifiedLine("TSE-Finish:", $"{result.TseResult.EndTime}")
					.AddJustifiedLine("TSE-Zeitformat:",$"{result.TseResult.TseTimestampFormat}")
					.AddLine($"TSE-Seriennummer: {result.TseResult.TseSerialNumber}")
					.AddJustifiedLine("TSE-Signaturcount:", $"{result.TseResult.SignatureCount}")
					.AddJustifiedLine("TSE-Algorithmus:", $"{result.TseResult.HashAlgorithm}")
					.AddLine($"TSE-PublicKey: {result.TseResult.PublicKey}")
					.AddDivider();
		}
		
		
		receipt.AddLine("Datum / Uhrzeit:")
				.AddLine(DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("de")));
				
		return receipt;
	}
}