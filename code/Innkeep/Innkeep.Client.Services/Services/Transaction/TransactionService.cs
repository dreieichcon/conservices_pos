using System.Collections.ObjectModel;
using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Client.Services.Interfaces.Hardware;
using Innkeep.Client.Services.Interfaces.Pretix;
using Innkeep.Client.Services.Interfaces.Server;
using Innkeep.Client.Services.Interfaces.Transaction;
using Innkeep.Models.Transaction;
using Innkeep.Server.Services.Legacy.Models;
using Innkeep.Server.Services.Legacy.Util;

namespace Innkeep.Client.Services.Services.Transaction;

public class TransactionService : ITransactionService
{
	private readonly IShoppingCartService _shoppingCartService;
	private readonly IClientPretixService _clientPretixService;
	private readonly IClientServerConnectionService _clientServerConnectionService;
	private readonly IPrintService _printService;

	public bool TestMode { get; set; }
	public decimal AmountDue { get; set; }
	public decimal AmountGiven { get; set; }
	public decimal AmountDueTax { get; set; }

	public decimal AmountBack { get; set; }

	public required string Currency { get; set; }

	public DateTime TransactionStarted { get; set; }

	public required ObservableCollection<TransactionItem> Items { get; set; }

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
		Items = new ObservableCollection<TransactionItem>(items);
		
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

		AmountBack = AmountGiven - AmountDue;
		
		if (TestMode)
		{
			_printService.LastReceipt = ReceiptGenerator.Generate(
				new TransactionServiceResult()
				{
					EventName = _clientPretixService.SelectedEvent!.Name.German,
					RegisterId = "TEST KASSE",
					OrganizerInfo = _clientPretixService.SelectedOrganizer!.Name,
					PretixTransaction = new PretixTransaction()
					{
						AmountGiven = AmountGiven,
						TransactionId = Guid.Empty,
						TransactionItems = _shoppingCartService.Cart,
					},
					OrderResponse = new PretixOrderResponse()
					{
						Code = "TESTBUCHUNG",
						Secret = "TESTBUCHUNG",
					},
					TseResult = null,
					Guid = Guid.Empty,
				}
			);

			return true;
		}
		
		var transaction = new PretixTransaction(_shoppingCartService.Cart, AmountGiven, TransactionStarted)
		{
			TransactionId = Guid.NewGuid(),
		};

		var result = await _clientServerConnectionService.SendTransaction(transaction);

		if (result == null)
		{
			_printService.LastReceipt = null;
			return false;
		}

		_printService.LastReceipt = result;
		return true;

	}
}