using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Transaction;

public enum VatRate
{
	[EnumMember(Value = "NORMAL")]
	Normal,

	[EnumMember(Value = "REDUCED_1")]
	Reduced1,

	[EnumMember(Value = "SPECIAL_RATE_1")]
	SpecialRate1,

	[EnumMember(Value = "SPECIAL_RATE_2")]
	SpecialRate2,

	[EnumMember(Value = "NULL")]
	Null,
}