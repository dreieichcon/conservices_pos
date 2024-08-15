using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Client.Interfaces.Pos;

public interface IClientPosService
{
	public event EventHandler? CartUpdated;
	
	public event EventHandler? CurrentAmountUpdated; 
	
	public event EventHandler? MoneyUpdated;

	public event EventHandler? PaymentStateUpdated;
	
	public int CurrentAmount { get; set; }
	
	public decimal MoneyRequired { get; }
	
	public decimal TaxInRequired { get; }
	
	public TransactionReceipt? LastReceipt { get; set; }
	
	public List<DtoSalesItem> CartItems { get; set; }
	
	public void AddToCart(DtoSalesItem item);

	public void ClearCart();

	public void InitiatePayment();

	public void ClearMoneyGiven();

	public void CancelTransaction();

	public Task<TransactionReceipt?> CommitTransaction(); 
	
	public bool IsPaymentMode { get; set; }
	
	public decimal MoneyGiven { get; set; }
	
	public decimal MoneyBack { get; }

	public decimal MoneyRemaining { get; }
}