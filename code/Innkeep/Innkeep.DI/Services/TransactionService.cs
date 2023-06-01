using System.Collections.ObjectModel;
using Innkeep.Core.DomainModels.Transaction;
using Innkeep.Core.Interfaces.Services;
using Innkeep.Core.Interfaces.Transaction;

namespace Innkeep.DI.Services;

public class TransactionService : ITransactionService
{
	private readonly IShoppingCartService _shoppingCartService;
	private readonly IPretixService _pretixService;

	public decimal AmountDue { get; set; }
	public decimal AmountGiven { get; set; }
	public decimal AmountDueTax { get; set; }
	public string Currency { get; set; }

	public ObservableCollection<ITransactionItem> Items { get; set; }

	public event EventHandler? TransactionUpdated;

	public TransactionService(IShoppingCartService shoppingCartService, IPretixService pretixService)
	{
		_shoppingCartService = shoppingCartService;
		_pretixService = pretixService;
	}

	public void Initialize()
	{
		AmountDue = _shoppingCartService.Cart.Sum(x => x.Price);
		AmountDueTax = _shoppingCartService.Cart.Sum(x => x.TaxPrice);
		AmountGiven = 0;
		Currency = _pretixService.SelectedEvent!.Currency;

		var items = _shoppingCartService.Cart.Select(x => new TransactionItem(x));
		Items = new ObservableCollection<ITransactionItem>(items);
		
		TransactionUpdated?.Invoke(null, EventArgs.Empty);
	}

	public void UpdateGivenAmount(decimal given)
	{
		AmountGiven += given;
		TransactionUpdated?.Invoke(null, EventArgs.Empty);
	}

	public void ClearTransaction()
	{
		AmountDue = 0;
		AmountGiven = 0;
		AmountDueTax = 0;
	}
}