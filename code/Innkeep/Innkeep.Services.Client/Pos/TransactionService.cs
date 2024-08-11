using Innkeep.Api.Models.Internal;
using Innkeep.Api.Server.Interfaces;
using Innkeep.Services.Client.Interfaces.Pos;

namespace Innkeep.Services.Client.Pos;

public partial class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
{
	private decimal _moneyGiven;

	public event EventHandler? MoneyUpdated; 
	
	public event EventHandler? PaymentStateUpdated;
	
	public bool IsPaymentMode { get; set; }
	
	public void ClearMoneyGiven()
	{
		MoneyGiven = 0;
		MoneyUpdated?.Invoke(this, EventArgs.Empty);
	}

	public void CancelTransaction()
	{
		IsPaymentMode = false;
		PaymentStateUpdated?.Invoke(this, EventArgs.Empty);
	}

	public async Task<TransactionReceipt> CommitTransaction()
	{
		var transaction = new ClientTransaction()
		{
			SalesItems = CartItems,
			AmountGiven = MoneyGiven,
		};

		return new TransactionReceipt();
	}

	public decimal MoneyRequired => CartItems.Sum(x => x.TotalPrice);

	public decimal TaxInRequired => CartItems.Sum(x => x.TotalTax);

	public decimal MoneyGiven
	{
		get => _moneyGiven;
		set
		{
			_moneyGiven = value;
			MoneyUpdated?.Invoke(this, EventArgs.Empty);
		}
	}

	public decimal MoneyBack => MoneyGiven - MoneyRequired;

	public decimal MoneyRemaining => MoneyRequired - MoneyGiven;

	
}