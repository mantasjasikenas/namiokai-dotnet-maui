using CommunityToolkit.Maui.Views;

namespace Namiokai.Views;

public partial class SelectUserPopup : Popup
{

    public SelectUserPopup()
    {
        InitializeComponent();
        BindingContext = this;

        
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            Settings.CurrentUser = Utils.ParseUser(radioButton.Value.ToString());
            this.Close();
        }
    }


    [RelayCommand]
    public void ImageButtonClicked(string user)
    {
        Settings.CurrentUser = Utils.ParseUser(user);
        this.Close();
    }
}