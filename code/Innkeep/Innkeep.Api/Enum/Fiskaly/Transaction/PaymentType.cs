using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Transaction;

public enum PaymentType
{
	[EnumMember(Value = "CASH")]
	Cash,
	
	[EnumMember(Value = "NON_CASH")]
	NonCash,
}