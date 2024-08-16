using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Services.Client.Interfaces.Checkin;

public interface ICheckinService
{
	public LinkedList<PretixCheckinResponse> LastCheckins { get; set; }
	
	public Task<PretixCheckinResponse?> CheckIn(string secret);
}