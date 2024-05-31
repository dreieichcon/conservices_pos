using Innkeep.Server.Data.Interfaces.Register;
using Innkeep.Server.Data.Models;
using Innkeep.Server.Services.Interfaces.Db;

namespace Innkeep.Server.Services.Services.Db;

public class RegisterService : IRegisterService
{
	private readonly IRegisterRepository _registerRepository;

	public RegisterService(IRegisterRepository registerRepository)
	{
		_registerRepository = registerRepository;
		CurrentRegisters = registerRepository.GetAll().ToList();
		PendingRegisters = new List<Register>();
	}
	
	public event EventHandler? RegistersChanged;
	
	public List<Register> CurrentRegisters { get; set; }
	
	public List<Register> PendingRegisters { get; set; }

	public void AddPendingRegister(string registerId)
	{
		var register = new Register()
		{
			DeviceId = registerId,
			Id = 0
		};
		
		PendingRegisters.Add(register);
		RegistersChanged?.Invoke(this, EventArgs.Empty);
	}

	public void UpdateRegister(Register register)
	{
		_registerRepository.Update(register);
		CurrentRegisters = _registerRepository.GetAll().ToList();
		RegistersChanged?.Invoke(this, EventArgs.Empty);
	}

	public void ConfirmPendingRegister(Register register)
	{
		_registerRepository.Create(register);
		PendingRegisters.Remove(register);
		CurrentRegisters = _registerRepository.GetAll().ToList();
		RegistersChanged?.Invoke(this, EventArgs.Empty);
	}

	public void RejectPendingRegister(Register register)
	{
		PendingRegisters.Remove(register);
		RegistersChanged?.Invoke(this, EventArgs.Empty);
	}

	public bool CurrentRegistersContains(string registerId, out Register? register)
	{
		register = CurrentRegisters.FirstOrDefault(x => x.DeviceId == registerId);
		return register is not null;
	}

	public bool CurrentRegistersContains(string registerId) 
		=> CurrentRegisters.Exists(x => x.DeviceId == registerId);

	public bool PendingRegistersContains(string registerId) 
		=> PendingRegisters.Exists(x => x.DeviceId == registerId);
}