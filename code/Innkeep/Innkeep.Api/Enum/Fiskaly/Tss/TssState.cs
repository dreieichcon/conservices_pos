using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Tss;

public enum TssState
{
	[EnumMember(Value = "CREATED")]
	Created,

	[EnumMember(Value = "UNINITIALIZED")]
	Uninitialized,

	[EnumMember(Value = "INITIALIZED")]
	Initialized,

	[EnumMember(Value = "DISABLED")]
	Disabled,

	[EnumMember(Value = "DELETED")]
	Deleted,

	[EnumMember(Value = "DEFECTIVE")]
	Defective,

	[EnumMember(Value = "EVICTED")]
	Evicted,
}