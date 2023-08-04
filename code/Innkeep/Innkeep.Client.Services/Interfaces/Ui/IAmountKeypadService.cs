namespace Innkeep.Client.Services.Interfaces.Ui;

public interface IAmountKeypadService
{
    public bool IsConfirmed { get; set; }
    
    
    public event EventHandler? AmountChanged;
    
    public string Amount { get; set; }

    public void SetAmount(string s);

    public void ClearAmount();
}