using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Services.Legacy.Interfaces.Db;

public interface IRegisterService
{
	public List<Register> CurrentRegisters { get; set; }
	
	public List<Register> PendingRegisters { get; set; }

	public event EventHandler RegistersChanged;

	public void AddPendingRegister(string registerId);

	public void UpdateRegister(Register register);

	public void ConfirmPendingRegister(Register register);

	public void RejectPendingRegister(Register register);
	
	public bool CurrentRegistersContains(string registerId);

	public bool CurrentRegistersContains(string registerId, out Register? register);

	public bool PendingRegistersContains(string registerId);
}