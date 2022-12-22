namespace Namiokai.ViewModels
{
    public partial class FuelViewModel : ObservableObject
    {
        private readonly SheetsManager sheetsManager;

        [ObservableProperty]
        bool isValid;
        [ObservableProperty]
        DateTime date = DateTime.Now;
        [ObservableProperty]
        string passengers;
        [ObservableProperty]
        bool tripToHome;
        [ObservableProperty]
        bool tripToKaunas;

        public FuelViewModel(SheetsManager sheetsManager)
        {
            this.sheetsManager = sheetsManager;
        }


        [RelayCommand]
        public async Task AppendFuelAsync()
        {
            if (string.IsNullOrEmpty(Passengers) || !(TripToHome || TripToKaunas))
            {
                await Shell.Current.CurrentPage.DisplayAlert(Resources.Error, Resources.All_properties_are_required, Resources.OK);
                return;
            }

            await sheetsManager.AppendFuelAsync(new Fuel(IsValid, Date, Passengers, TripToHome, TripToKaunas));
            Clear();


        }
        private void Clear()
        {
            IsValid = default;
            Date = DateTime.Now;
            Passengers = string.Empty;
            TripToHome = default;
            TripToKaunas = default;
        }
    }
}
