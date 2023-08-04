using System.Collections.ObjectModel;
using Innkeep.Core.Interfaces.Transaction;

namespace Innkeep.Client.Services.Interfaces.Transaction;

public interface ITransactionService
{
	public decimal AmountDue { get; set; }
	
	public decimal AmountGiven { get; set; }
	
	public decimal AmountDueTax { get; set; }
	
	public string Currency { get; set; }
	
	public DateTime TransactionStarted { get; set; }
	
	public ObservableCollection<ITransactionItem> Items { get; set; }

	public event EventHandler TransactionUpdated;

	public void UpdateGivenAmount(decimal given);

	public void Initialize();

	public void ClearTransaction();

	public Task<bool> CommitTransaction();

}