using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Transaction;

public enum VatRate
{
	[EnumMember(Value = "NORMAL")]
	Normal,

	[EnumMember(Value = "REDUCED_1")]
	Reduced_1,

	[EnumMember(Value = "SPECIAL_RATE_1")]
	Special_Rate_1,

	[EnumMember(Value = "SPECIAL_RATE_2")]
	Special_Rate_2,

	[EnumMember(Value = "NULL")]
	Null,
}