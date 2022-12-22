using CommunityToolkit.Maui.Views;
using Namiokai.Services;
using Namiokai.ViewModels;

namespace Namiokai.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel vm;
    private readonly SheetsManager sheetsManager;

    public MainPage(MainViewModel vm, SheetsManager sheetsManager)
    {
       
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
        this.sheetsManager = sheetsManager;
        
        vm.GetDebtsCacheAsync();
    }

    private async void ContentPage_Appearing(object sender, EventArgs e)
    {
        await vm.GetDebtsAsync();

        if (Settings.CurrentUser != User.Unspecified) return;
        
        SelectUserPopup pop = new()
        {
            Size = new Size(Width - 30, Height / 2 - 120)
        };

        await this.ShowPopupAsync(pop);

    }
    // private async void SettingsToolBar_Clicked(object sender, EventArgs e)
    // {
    //     await Shell.Current.GoToAsync(nameof(SettingsPage));
    // }
    //
    // private async void SheetsToolBar_Clicked(object sender, EventArgs e)
    // {
    //     await Browser.Default.OpenAsync(Strings.Resources.Sheets_URL);
    // }




}

