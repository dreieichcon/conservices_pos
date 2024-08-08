using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Client;

public enum ClientState
{
	[EnumMember(Value = "REGISTERED")]
	Registered,
	
	[EnumMember(Value = "DEREGISTERED")]
	Deregistered,
}