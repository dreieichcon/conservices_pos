namespace Innkeep.Client.Interfaces.Services;

public interface IAmountKeypadService
{
    public bool IsConfirmed { get; set; }
    
    
    public event EventHandler? AmountChanged;
    
    public string Amount { get; set; }

    public void SetAmount(string s);

    public void ClearAmount();
}