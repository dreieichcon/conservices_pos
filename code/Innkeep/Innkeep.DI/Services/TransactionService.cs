using System.Collections.ObjectModel;
using Innkeep.Client.Interfaces.Services;
using Innkeep.Core.Interfaces.Transaction;
using Innkeep.Server.Interfaces.Services;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.DI.Services;

public class TransactionService : ITransactionService
{
	private readonly IShoppingCartService _shoppingCartService;
	private readonly IClientPretixService _clientPretixService;

	public decimal AmountDue { get; set; }
	public decimal AmountGiven { get; set; }
	public decimal AmountDueTax { get; set; }
	public string Currency { get; set; }

	public ObservableCollection<ITransactionItem> Items { get; set; }

	public event EventHandler? TransactionUpdated;

	public TransactionService(IShoppingCartService shoppingCartService, IClientPretixService clientPretixService)
	{
		_shoppingCartService = shoppingCartService;
		_clientPretixService = clientPretixService;
	}

	public void Initialize()
	{
		AmountDue = _shoppingCartService.Cart.Sum(x => x.Price);
		AmountDueTax = _shoppingCartService.Cart.Sum(x => x.TaxPrice);
		AmountGiven = 0;
		Currency = _clientPretixService.SelectedEvent!.Currency;

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