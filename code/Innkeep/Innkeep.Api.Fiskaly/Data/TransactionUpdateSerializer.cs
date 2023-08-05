using Innkeep.Api.Fiskaly.Enums;
using Innkeep.Api.Fiskaly.Models;
using Innkeep.Api.Fiskaly.Models.Fiskaly;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Fiskaly.Data;

public static class TransactionUpdateSerializer
{
	public static TransactionUpdateRequestModel Create(string clientId, PretixTransaction transaction)
	{
		return new TransactionUpdateRequestModel()
		{
			Schema = new Schema()
			{
				StandardV1 = new StandardV1()
				{
					receipt = new Receipt()
					{
						ReceiptType = "RECEIPT",
						AmountsPerVatRate = CreateAmountsPerVatRate(transaction),
						AmountsPerPaymentType = new List<AmountsPerPaymentType>()
						{
							new()
							{
								PaymentType = PaymentType.CASH,
								DecimalAmount = transaction.Sum,
							}
						}
					}
				}
			},
			State = TransactionState.ACTIVE,
			ClientId = clientId
		};
	}

	private static List<AmountsPerVatRate> CreateAmountsPerVatRate(PretixTransaction pretixTransaction)
	{
		var amountsPerVatRate = new List<AmountsPerVatRate>();
		
		var transactions = pretixTransaction.TransactionItems.GroupBy(x => x.Item.TaxRate);

		foreach (var group in transactions)
		{
			amountsPerVatRate.Add(new AmountsPerVatRate()
			{
				DecimalAmount = group.Sum(x => x.Price),
				VatRate = VatRateConverter.Get(group.First().Item.TaxRate)
			});
		}

		return amountsPerVatRate;
	}
}