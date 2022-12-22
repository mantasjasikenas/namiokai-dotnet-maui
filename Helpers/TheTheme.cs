

namespace Namiokai.Helpers;

public static class TheTheme
{
    public static void SetTheme()
    {
        var appTheme = Settings.Theme;
        Application.Current.UserAppTheme = appTheme;
    }
}