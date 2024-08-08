using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Transaction;

public enum TransactionState
{
	[EnumMember(Value = "ACTIVE")]
	Active,

	[EnumMember(Value = "CANCELLED")]
	Cancelled,

	[EnumMember(Value = "FINISHED")]
	Finished,
}