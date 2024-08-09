using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Fiskaly.Interfaces.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Services.Server.Interfaces.Fiskaly;

namespace Innkeep.Services.Server.Fiskaly;

public class FiskalyTransactionService(
	IFiskalyTransactionRepository transactionRepository,
	IFiskalyClientService clientService,
	IFiskalyTssService tssService
) : IFiskalyTransactionService
{
	private FiskalyClient CurrentClient => clientService.CurrentClient!;

	private FiskalyTss CurrentTss => tssService.CurrentTss!;

	private FiskalyTransaction? CurrentTransaction { get; set; }

	private int TransactionRevision { get; set; } = 1;

	public async Task<bool> StartTransaction()
	{
		var transactionGuid = Guid.NewGuid();

		CurrentTransaction = await transactionRepository.StartTransaction(
			CurrentTss.Id,
			transactionGuid.ToString(),
			CurrentClient.Id
		);

		if (CurrentTransaction != null)
			TransactionRevision++;
		
		return CurrentTransaction != null;
	}

	public async Task<TransactionReceipt> CompleteTransaction(ClientTransaction transaction)
	{
		var request = new FiskalyTransactionUpdateRequest()
		{
			ClientId = CurrentClient.Id,
			TssId = CurrentTss.Id,
			Schema = new FiskalyTransactionSchema
			{
				StandardV1 = new FiskalySchemaStandardV1()
				{
					Receipt = new FiskalyReceipt()
					{
						ReceiptType = ReceiptType.Receipt,
						AmountsPerVatRate = new List<FiskalyAmountPerVatRate>()
						{
							new FiskalyAmountPerVatRate()
							{
								Amount = 0,
								VatRate = VatRate.Null,
							},
						},
					},
				},
			},
			State = TransactionState.Finished,
		};

		var result = await transactionRepository.UpdateTransaction(request);

		return new TransactionReceipt();
	}

	
}