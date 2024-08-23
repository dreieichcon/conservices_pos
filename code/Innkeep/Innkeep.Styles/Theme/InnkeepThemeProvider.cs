using MudBlazor;
using MudBlazor.Utilities;

namespace Innkeep.Styles.Theme;

public static class InnkeepThemeProvider
{
	public static MudTheme GetTheme()
	{
		return new MudTheme()
		{
			PaletteLight = new PaletteLight()
			{
				Primary = new MudColor("#196889"),
			},
			PaletteDark = new PaletteDark(),
		};

	}
}