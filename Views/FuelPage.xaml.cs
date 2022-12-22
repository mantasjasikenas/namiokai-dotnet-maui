namespace Namiokai.Views;

public partial class FuelPage : ContentPage
{
	public FuelPage(FuelViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}