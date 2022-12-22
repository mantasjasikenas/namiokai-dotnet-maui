namespace Namiokai.Helpers;

public static class Settings
{
    public static User CurrentUser
    {
        get
        {
            if (!Preferences.ContainsKey(nameof(CurrentUser)))
                return User.Unspecified;

            return Enum.Parse<User>(Preferences.Get(nameof(CurrentUser), User.Unspecified.ToString()));
        }
        set => Preferences.Set(nameof(CurrentUser), value.ToString());
    }


    public static AppTheme Theme
    {
        get
        {
            if (!Preferences.ContainsKey(nameof(Theme)))
            {
                if (Application.Current != null)
                {
                    var deviceTheme = Application.Current.RequestedTheme;
                    return deviceTheme == AppTheme.Unspecified ? AppTheme.Light : deviceTheme;
                }
            }

            return Enum.Parse<AppTheme>(Preferences.Get(nameof(Theme), Enum.GetName(AppTheme.Light))!);
        }
        set => Preferences.Set(nameof(Theme), value.ToString());
    }

}
