using Innkeep.Core.Interfaces.Services;

namespace Innkeep.DI.Services;

public class AmountKeypadService : IAmountKeypadService
{
    public bool IsConfirmed { get; set; } = true;
    
    public event EventHandler? AmountChanged;

    public string Amount { get; set; } = "1";

    private bool Reset { get; set; } = true;
    
    public void SetAmount(string s)
    {
        if (Reset)
        {
            Amount = s;
            Reset = false;
        }
        else
        {
            Amount += s;
        }
        
        IsConfirmed = false;
        AmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClearAmount()
    {
        Amount = "1";
        Reset = true;
        IsConfirmed = true;
        AmountChanged?.Invoke(this, EventArgs.Empty);
    }
}