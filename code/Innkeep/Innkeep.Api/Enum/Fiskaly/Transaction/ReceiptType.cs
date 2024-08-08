using System.Runtime.Serialization;

namespace Innkeep.Api.Enum.Fiskaly.Transaction;

public enum ReceiptType
{
	[EnumMember(Value = "RECEIPT")]
	Receipt,

	[EnumMember(Value = "TRAINING")]
	Training,

	[EnumMember(Value = "TRANSFER")]
	Transfer,

	[EnumMember(Value = "ORDER")]
	Order,

	[EnumMember(Value = "CANCELLATION")]
	Cancellation,

	[EnumMember(Value = "ABORT")]
	Abort,

	[EnumMember(Value = "BENEFIT_IN_KIND")]
	BenefitInKind,

	[EnumMember(Value = "INVOICE")]
	Invoice,

	[EnumMember(Value = "OTHER")]
	Other,

	[EnumMember(Value = "ANNULATION")]
	Annulation,
}