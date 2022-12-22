namespace Namiokai;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

    }

    private async void SettingsToolBar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    private async void SheetsToolBar_Clicked(object sender, EventArgs e)
    {
        await Browser.Default.OpenAsync(Namiokai.Resources.Sheets_URL);
    }

}
