
namespace Namiokai;

public class MainWindow : Window
{
    public MainWindow() : base()
    {

    }

    public MainWindow(Page page) : base(page)
    {

    }
                                 
    protected override void OnCreated()
    {
        base.OnCreated();
        SetUserFromStorage();
    }

    private async void SetUserFromStorage()
    {
        var user = await SecureStorage.GetAsync(nameof(AuthUser));
        try
        {
            if (user is not null)
            {
                LoginManager.AuthUser = new AuthUser(user);
            }
        }
        catch
        {

        }
    }
}
