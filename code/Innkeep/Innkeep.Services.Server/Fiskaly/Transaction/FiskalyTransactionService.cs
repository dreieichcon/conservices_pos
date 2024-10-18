using System.Globalization;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Fiskaly.Interfaces.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Client;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Tss;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Internal.Transaction;
using Innkeep.Services.Server.Interfaces.Fiskaly.Client;
using Innkeep.Services.Server.Interfaces.Fiskaly.Transaction;
using Innkeep.Services.Server.Interfaces.Fiskaly.Tss;

namespace Innkeep.Services.Server.Fiskaly.Transaction;

public partial class FiskalyTransactionService(
	IFiskalyTransactionRepository transactionRepository,
	IFiskalyClientService clientService,
	IFiskalyTssService tssService
) : IFiskalyTransactionService
{
	private FiskalyClient? CurrentClient => clientService.CurrentClient;

	private FiskalyTss? CurrentTss => tssService.CurrentTss;

	private FiskalyTransaction? CurrentTransaction { get; set; }

	private int TransactionRevision { get; set; } = 1;

	public async Task<FiskalyTransaction?> StartTransaction()
	{
		var transactionGuid = Guid.NewGuid();
		CurrentTransaction = null;
		TransactionRevision = 1;

		if (CurrentTss is null || CurrentClient is null)
			return null;

		var result = await transactionRepository.StartTransaction(
			CurrentTss!.Id,
			transactionGuid.ToString(),
			CurrentClient!.Id
		);

		if (!result.IsSuccess)
			return null;

		CurrentTransaction = result.Object!;
		TransactionRevision++;

		return CurrentTransaction;
	}

	public async Task<TransactionReceipt?> CompleteReceiptTransaction(ClientTransaction transaction)
	{
		TransactionReceipt? receipt = null;

		if (CurrentTransaction is not null)
		{
			var request = new FiskalyTransactionUpdateRequest
			{
				TransactionRevision = TransactionRevision,
				TransactionId = CurrentTransaction?.Id!,
				ClientId = CurrentClient.Id,
				TssId = CurrentTss.Id,
				Schema = new FiskalyTransactionSchema
				{
					StandardV1 = new FiskalySchemaStandardV1
					{
						Receipt = new FiskalyReceipt
						{
							ReceiptType = ReceiptType.Receipt,
							AmountsPerVatRate = TransactionVatRates(transaction),
							AmountsPerPaymentType = TransactionPaymentTypes(transaction),
						},
					},
				},
				State = TransactionState.Finished,
			};

			var result = await transactionRepository.UpdateTransaction(request);
			receipt = CreateTransactionReceipt(result.Object, transaction);
		}

		if (CurrentTransaction is null)
			receipt = CreateTransactionReceipt(null, transaction);

		return receipt;
	}

	public void FinalizeTransactionFlow()
	{
		TransactionRevision = 1;
		CurrentTransaction = null;
	}

	private static List<FiskalyAmountPerVatRate> TransactionVatRates(ClientTransaction transaction)
	{
		var list = new List<FiskalyAmountPerVatRate>();

		var groups = transaction.SalesItems.GroupBy(x => x.TaxRate);

		foreach (var group in groups)
		{
			var vatRate = group.Key switch
			{
				19m => VatRate.Normal,
				7m => VatRate.Reduced_1,
				0 => VatRate.Null,
				var _ => VatRate.Null,
			};

			var sum = group.Sum(x => x.TotalPrice);

			list.Add(
				new FiskalyAmountPerVatRate
				{
					Amount = sum,
					VatRate = vatRate,
				}
			);
		}

		return list;
	}

	private static List<FiskalyAmountPerPaymentType> TransactionPaymentTypes(ClientTransaction transaction)
	{
		var list = new List<FiskalyAmountPerPaymentType>();

		list.Add(
			new FiskalyAmountPerPaymentType
			{
				Amount = transaction.AmountNeeded,
				PaymentType = transaction.PaymentType,
			}
		);

		return list;
	}

	private static TransactionReceipt CreateTransactionReceipt(
		FiskalyTransaction? fiskalyTransaction,
		ClientTransaction clientTransaction
	) => new()
	{
		TransactionCounter = fiskalyTransaction?.Number ?? -1,
		Lines = CreateLines(clientTransaction),
		TaxInformation = CreateTaxInformation(clientTransaction),
		Sum = CreateSum(clientTransaction),
		QrCode = fiskalyTransaction?.QrCodeData ?? "TSS ERROR",
	};

	private static List<ReceiptLine> CreateLines(ClientTransaction transaction)
		=> transaction.SalesItems.Select(ReceiptLine.FromCart).ToList();

	private static List<ReceiptTaxInformation> CreateTaxInformation(ClientTransaction transaction)
	{
		var groups = transaction.SalesItems.GroupBy(x => x.TaxRate);

		return groups
				.Select(
					group => new ReceiptTaxInformation
					{
						Name = (group.Key / 100).ToString("P0", CultureInfo.InvariantCulture),
						Net = group.Sum(x => x.NetPrice),
						TaxAmount = group.Sum(x => x.TaxAmount),
						Gross = group.Sum(x => x.TotalPrice),
					}
				)
				.ToList();
	}

	private static ReceiptSum CreateSum(ClientTransaction transaction) => new()
	{
		TotalAmount = transaction.AmountNeeded,
		AmountReturned = transaction.AmountBack,
		AmountGiven = transaction.AmountGiven,
	};
}