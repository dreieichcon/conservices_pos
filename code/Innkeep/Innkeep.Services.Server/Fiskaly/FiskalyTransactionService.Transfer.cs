using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transfer;

namespace Innkeep.Services.Server.Fiskaly;

public partial class FiskalyTransactionService
{
	public async Task<TransferReceipt?> CompleteTransferTransaction()
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
						ReceiptType = ReceiptType.Receipt,
						AmountsPerVatRate = [],//TransferVatRates(transaction),
						AmountsPerPaymentType = [],//TransferPaymentTypes(transaction),
					},
				},
			},
			State = TransactionState.Finished,
		};

		var result = await transactionRepository.UpdateTransaction(request);

		return new();
	}
}