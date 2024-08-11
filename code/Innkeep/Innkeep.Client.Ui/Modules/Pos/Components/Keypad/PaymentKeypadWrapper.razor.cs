using System.Globalization;

namespace Innkeep.Client.Ui.Modules.Pos.Components.Keypad;

public partial class PaymentKeypadWrapper
{
	private string _currentAmountString = "0";
	
	private bool _valid;
	private decimal CurrentAmount { get; set; }

	private string CurrentAmountString
	{
		get => _currentAmountString;
		set
		{
			_currentAmountString = value;

			if (decimal.TryParse(_currentAmountString, CultureInfo.InvariantCulture, out var val))
			{
				CurrentAmount = val;
				_valid = true;
			}
			else
			{
				_valid = false;
			}
				
		}
	}

	private bool DecimalDisabled => CurrentAmountString.Contains('.');
	
	public async Task TransferAmount()
	{
		TransactionService.MoneyGiven += CurrentAmount;
		CurrentAmountString = "0";
		await InvokeAsync(StateHasChanged);
	}

	public async Task ClearAmount()
	{
		CurrentAmountString = "0";
		await InvokeAsync(StateHasChanged);
	}

	public void SetAmount(string amount)
	{
		if (CurrentAmountString == "0")
		{
			CurrentAmountString = amount == "." ? $"0{amount}" : amount;
		}
		else
		{
			CurrentAmountString += amount;
		}
	}
}