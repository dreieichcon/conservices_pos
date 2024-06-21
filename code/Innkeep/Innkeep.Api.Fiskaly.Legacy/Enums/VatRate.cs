namespace Innkeep.Api.Fiskaly.Legacy.Enums;

public enum VatRate
{
	NORMAL,
	NULL
}

public static class VatRateConverter
{
	public static VatRate Get(decimal vatRate)
	{
		switch (vatRate)
		{
			case (0):
				return VatRate.NULL;
			
			case (19):
			default:
				return VatRate.NORMAL;
		}
	}
}