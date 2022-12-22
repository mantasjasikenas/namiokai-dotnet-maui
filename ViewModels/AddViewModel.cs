using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Namiokai.Models;
using Namiokai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Namiokai.Views;

namespace Namiokai.ViewModels
{
    public partial class AddViewModel : ObservableObject
    {
        private readonly SheetsManager sheetsManager;

        [ObservableProperty]
        string whoPaid;
        [ObservableProperty]
        string shoppingList;
        [ObservableProperty]
        string totalPrice;
        [ObservableProperty]
        bool mantelisSplit;
        [ObservableProperty]
        bool klaidasSplit;
        [ObservableProperty]
        bool klaidelisSplit;


        public AddViewModel(SheetsManager sheetsManager)
        {
            this.sheetsManager = sheetsManager;
            WhoPaid = Settings.CurrentUser.ToString();
        }

        [RelayCommand]
        public async Task AppendBillAsync()
        {
            if (WhoPaid.Equals(User.Unspecified.ToString()) ||
                string.IsNullOrEmpty(ShoppingList) ||
                string.IsNullOrEmpty(TotalPrice) ||
                !(KlaidasSplit || MantelisSplit || KlaidelisSplit))
            {
                await Shell.Current.CurrentPage.DisplayAlert(Resources.Error, Resources.All_properties_are_required, Resources.OK);
                return;
            }

            try
            {
                ShoppingItem item = new(DateTime.Now.ToString("yyyy/MM/dd"), WhoPaid, ShoppingList, TotalPrice, MantelisSplit, KlaidasSplit, KlaidelisSplit);
                await sheetsManager.AppendShoppingItemAsync(item);
                Clear();
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                // ignored
            }

        }

        private void Clear()
        {
            WhoPaid = Settings.CurrentUser.ToString();
            ShoppingList = string.Empty;
            TotalPrice = string.Empty;
            MantelisSplit = false;
            KlaidasSplit = false;
            KlaidelisSplit = false;
        }
    }
}
