using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Interfaces;

public interface IRegisterService
{
	public List<Register> CurrentRegisters { get; set; }
	
	public List<Register> PendingRegisters { get; set; }

	public event EventHandler RegistersChanged;

	public void AddPendingRegister(string registerId);

	public void ConfirmPendingRegister(Register register);

	public void RejectPendingRegister(Register register);

	public bool CurrentRegistersContains(string registerId);

	public bool PendingRegistersContains(string registerId);
}