using System.Collections.ObjectModel;
using Innkeep.Client.Interfaces.Services;
using Innkeep.Core.Interfaces.Transaction;
using Innkeep.Server.Data.Models;
using Innkeep.Shared.Objects.Transaction;
using Transaction = Innkeep.Shared.Objects.Transaction.Transaction;

namespace Innkeep.DI.Services;

public class TransactionService : ITransactionService
{
	private readonly IShoppingCartService _shoppingCartService;
	private readonly IClientPretixService _clientPretixService;
	private readonly IClientServerConnectionService _clientServerConnectionService;
	private readonly IPrintService _printService;

	public decimal AmountDue { get; set; }
	public decimal AmountGiven { get; set; }
	public decimal AmountDueTax { get; set; }
	public string Currency { get; set; }

	public ObservableCollection<ITransactionItem> Items { get; set; }

	public event EventHandler? TransactionUpdated;

	public TransactionService(IShoppingCartService shoppingCartService, 
							IClientPretixService clientPretixService,
							IClientServerConnectionService clientServerConnectionService,
							IPrintService printService)
	{
		_shoppingCartService = shoppingCartService;
		_clientPretixService = clientPretixService;
		_clientServerConnectionService = clientServerConnectionService;
		_printService = printService;
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

	public async Task<bool> CommitTransaction()
	{
		var transaction = new Transaction(_shoppingCartService.Cart, AmountGiven);

		var result = await _clientServerConnectionService.SendTransaction(transaction);
		if (result != null)
		{
			
			// TODO - PRINT
			_printService.Print(result);
			return true;
		}

		return false;
	}
}