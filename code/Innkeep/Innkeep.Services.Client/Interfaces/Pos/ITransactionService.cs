using Innkeep.Api.Models.Internal;

namespace Innkeep.Services.Client.Interfaces.Pos;

public interface ITransactionService
{
	public Task AddToCart(DtoSalesItem item);

	public Task ClearCart();

	public Task InitiatePayment();

	public Task CommitTransaction(); 
	
	public int CurrentAmount { get; set; }
	
	public decimal MoneyGiven { get; set; }
	
	public decimal MoneyBack { get; set; }

	public List<DtoSalesItem> CartItems();
}