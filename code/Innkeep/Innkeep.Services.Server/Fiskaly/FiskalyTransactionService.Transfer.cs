using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Enum.Shared;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Services.Server.Fiskaly;

public partial class FiskalyTransactionService
{
	public async Task<TransferReceipt> CompleteTransferTransaction(ClientTransfer model)
	{
		var request = new FiskalyTransactionUpdateRequest()
		{
			TransactionRevision = TransactionRevision,
			TransactionId = CurrentTransaction?.Id!,
			ClientId = CurrentClient.Id,
			TssId = CurrentTss.Id,
			Schema = new FiskalyTransactionSchema
			{
				StandardV1 = new FiskalySchemaStandardV1()
				{
					Receipt = new FiskalyReceipt()
					{
						ReceiptType = ReceiptType.Transfer,
						AmountsPerVatRate = TransferVatRates(model.Amount * model.Factor),
						AmountsPerPaymentType = TransferPaymentTypes(model.Amount * model.Factor),
					},
				},
			},
			State = TransactionState.Finished,
		};

		var result = await transactionRepository.UpdateTransaction(request);
		
		return new TransferReceipt
		{
			Amount = model.Amount,
			IsRetrieve = model.IsRetrieve,
			BookingTime = DateTime.Now,
			TransactionCounter = result?.Number ?? -1,
			QrCode = result?.QrCodeData ?? "TSS ERROR",
		};
	}

	private static List<FiskalyAmountPerVatRate> TransferVatRates(decimal amount)
	{
		var list = new List<FiskalyAmountPerVatRate>
		{
			new ()
			{
				Amount = amount,
				VatRate = VatRate.Null,
			},
		};

		return list;
	}

	private static List<FiskalyAmountPerPaymentType> TransferPaymentTypes(decimal amount)
	{
		var list = new List<FiskalyAmountPerPaymentType>
		{
			new()
			{
				Amount = amount,
				CurrencyCode = CurrencyCode.EUR,
				PaymentType = PaymentType.Cash,
			},
		};

		return list;
	}
}