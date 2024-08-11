using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Client.Pos;

public partial class TransactionService
{
	public event EventHandler? CartUpdated;

	public event EventHandler? CurrentAmountUpdated;
	
	public List<DtoSalesItem> CartItems { get; set; } = [];
	
	public int CurrentAmount { get; set; } = 1;
	
	public void AddToCart(DtoSalesItem item)
	{
		var exists = CartItems.Find(x => x.Id == item.Id);

		if (exists is not null)
		{
			exists.CartCount += CurrentAmount;
			UpdateCartAndAmounts();
			return;
		}

		item.CartCount = CurrentAmount;
		CartItems.Add(item);

		CurrentAmount = 1;
		UpdateCartAndAmounts();
	}

	public void ClearCart()
	{
		CartItems = [];
		CurrentAmount = 1;
		UpdateCartAndAmounts();
	}
	
	public void InitiatePayment()
	{
		IsPaymentMode = true;
		PaymentStateUpdated?.Invoke(this, EventArgs.Empty);
	}
	
	private void UpdateCartAndAmounts()
	{
		CartUpdated?.Invoke(this, EventArgs.Empty);
		CurrentAmountUpdated?.Invoke(this, EventArgs.Empty);
	}
}