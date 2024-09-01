namespace Innkeep.Client.Ui.Modules.Pos.Components;

public static class PosStyles
{
	private static int DisplayWidth => 33;
	public static string DisplayStyle => $"width: {DisplayWidth}% !important; max-width: {DisplayWidth}% !important";
	
	public static string Wider => $"width: {50}% !important; max-width: {50}% !important";
	public static string DisplayStyleDouble => $"width: {2*DisplayWidth + 1}% !important; max-width: {2*DisplayWidth + 1}% !important";

	public static string KeypadStyle => "width: 100% !important; height: 70% !important;";
	
	public static string KeypadRowStyle => "width: 100% !important; height: 20% !important";

	public static string KeypadButtonStyle => "width: 25% !important;";
}