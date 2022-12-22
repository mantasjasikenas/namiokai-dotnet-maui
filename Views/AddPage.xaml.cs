using Namiokai.ViewModels;

namespace Namiokai.Views;

public partial class AddPage : ContentPage
{
    public AddPage(AddViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}