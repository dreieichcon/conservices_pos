using System.Globalization;
using Innkeep.Models.Printer;
using Innkeep.Server.Services.Models;

namespace Innkeep.Server.Services.Util;

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

		receipt.AddBlank().AddDivider();

		var sum = $"Summe:      {result.PretixTransaction.Sum.ToString(CultureInfo.InvariantCulture),6}€";
		var bck = $"Rück:       {result.PretixTransaction.Return.ToString(CultureInfo.InvariantCulture).PadLeft(6, ' ').Replace("-", "")}€";
		
		var giv = $"Erhalten:   {$"{result.PretixTransaction.AmountGiven:0.##}",6}€";
		
		
		receipt.AddSum(sum.PadLeft(42, ' '))
				.AddSum(giv.PadLeft(42, ' '))
				.AddSum(bck.PadLeft(42, ' '))
				.AddBlank()
				.AddDivider()
				.AddCenteredLine($"Order ID: {result.OrderResponse.Code}")
				.AddCenteredLine($"Order GUID: {result.Guid}")
				.AddBlank()
				.AddDivider();

		/// TSE DATA
		receipt.AddLine($"TSE-Signatur: {result.TseResult.Signature}")
				.AddLine($"TSE-Transaktionsnummer: {result.TseResult.TseTransactionNumber}")
				.AddLine($"TSE-Start: {result.TseResult.StartTime}")
				.AddLine($"TSE-Finish: {result.TseResult.EndTime}")
				.AddLine($"TSE-Seriennummer: {result.TseResult.TseSerialNumber}")
				.AddLine($"TSE-Signaturcount: {result.TseResult}")
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