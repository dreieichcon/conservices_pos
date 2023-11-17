using System.Collections.ObjectModel;
using Innkeep.Models.Transaction;

namespace Innkeep.Client.Services.Interfaces.Transaction;

public interface ITransactionService
{
	public bool TestMode { get; set; }
	
	public decimal AmountDue { get; set; }
	
	public decimal AmountGiven { get; set; }
	
	public decimal AmountDueTax { get; set; }
	
	public decimal AmountBack { get; set; }
	
	public string Currency { get; set; }
	
	public DateTime TransactionStarted { get; set; }
	
	public ObservableCollection<TransactionItem> Items { get; set; }

	public event EventHandler TransactionUpdated;

	public void UpdateGivenAmount(decimal given);

	public void Initialize();

	public void ClearTransaction();

	public Task<bool> CommitTransaction();

}