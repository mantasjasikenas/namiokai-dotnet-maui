using CommunityToolkit.Maui.Alerts;

namespace Namiokai.ViewModels;

[ObservableObject]
public partial class SettingsViewModel
{
    [ObservableProperty]
    bool isDarkModeEnabled;

    [ObservableProperty]
    string currentUser;

    [NotifyPropertyChangedFor(nameof(IsDevMenuHidden))]
    [ObservableProperty]
    bool isDevMenuVisible;

    public bool IsDevMenuHidden => !IsDevMenuVisible;


    private readonly SheetsManager sheetsManager;
    private readonly LoginManager loginManager;

    partial void OnIsDarkModeEnabledChanged(bool value) =>
        ChangeUserAppTheme(value);

    partial void OnCurrentUserChanged(string value) =>
        ChangeCurrentUser(value);

                                                              

    public SettingsViewModel(SheetsManager sheetsManager, LoginManager loginManager)
    {
        IsDarkModeEnabled = Settings.Theme == AppTheme.Dark;
        CurrentUser = Settings.CurrentUser.ToString();
        IsDevMenuVisible = LoginManager.AuthUser.Admin;


        this.sheetsManager = sheetsManager;
        this.loginManager = loginManager;
    }

    [RelayCommand]
    void ChangeUserAppTheme(bool activateDarkMode)
    {
        Settings.Theme = activateDarkMode
            ? AppTheme.Dark
            : AppTheme.Light;

        TheTheme.SetTheme();
    }

    [RelayCommand]
    void ChangeCurrentUser(string user) => Settings.CurrentUser = Enum.Parse<User>(user);

    [RelayCommand]
    public async Task ClearSheetsAsync() => await sheetsManager.ClearSheetsAsync();

    [RelayCommand]
    public async Task LoginAsync()
    {
        await loginManager.LoginAsync();
        IsDevMenuVisible = LoginManager.AuthUser.Admin;
        if (IsDevMenuVisible)
        {
            await Toast.Make(Resources.Dev_menu_toggled).Show();
        }

    }

    [RelayCommand]
    public async Task LogoutAsync()
    {
        await loginManager.LogoutAsync();
        IsDevMenuVisible = false;
    }

    [RelayCommand]
    public async Task ClearPreferencesAsync()
    {
        Preferences.Clear();
        CurrentUser = Settings.CurrentUser.ToString();
        IsDarkModeEnabled = Settings.Theme == AppTheme.Dark;
    }

}
