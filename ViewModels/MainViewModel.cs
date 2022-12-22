namespace Namiokai.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly SheetsManager sheetsManager;

    [ObservableProperty]
    Debts debts = new();

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    List<string> item = new();

    [ObservableProperty]
    Dictionary<User, string> users = new();

    public MainViewModel(SheetsManager sheetsManager)
    {
        this.sheetsManager = sheetsManager;
        Users.Add(User.Mantelis, Resources.Mantelis_avatar);
        Users.Add(User.Klaidelis, Resources.Klaidelis_avatar);
        Users.Add(User.Klaidas, Resources.Klaidas_avatar);


    }

    public async Task GetDebtsAsync()
    {
        IsRefreshing = true;
        Debts = await sheetsManager.GetSummaryDebtsAsync();
        IsRefreshing = false;

        // writes to cache
        Utils.WriteToCacheAsync(Debts.ToString());
    }

    public async Task GetDebtsCacheAsync()
    {
        var cache = await Utils.ReadFromCacheAsync();

        if (!string.IsNullOrEmpty(cache))
        {
            Debts = new Debts(cache);
        }
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await GetDebtsAsync();
        IsRefreshing = false;
    }
}
