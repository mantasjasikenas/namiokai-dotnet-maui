using Namiokai.Auth0;
using Namiokai.Services;

namespace Namiokai;

public partial class App : Application
{
    private readonly IConnectivity connectivity;
    private readonly Auth0Client client;

    public App(IConnectivity connectivity, Auth0Client client)
    {
        InitializeComponent();
        TheTheme.SetTheme();

        this.connectivity = connectivity;
        this.client = client;

        MainPage = (connectivity.NetworkAccess == NetworkAccess.Internet) ? new AppShell() : new NoInternet();

        connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

    }

    private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        MainPage = (e.NetworkAccess == NetworkAccess.Internet) ? new AppShell() : new NoInternet();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        //Window window = base.CreateWindow(activationState);
        Window window = new MainWindow(MainPage);

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            // Manipulate Window object
            const int newWidth = 362;
            const int newHeight = 756;

            // get screen size
            var disp = DeviceDisplay.Current.MainDisplayInfo;

            // center the window
            window.X = (disp.Width / disp.Density - newWidth) / 2;
            window.Y = (disp.Height / disp.Density - newHeight) / 2;

            // resize
            window.MinimumWidth = 362;
            window.MinimumHeight = 756;


            window.MaximumWidth = 1920;
            window.MaximumHeight = 1080;

            window.Width = newWidth;
            window.Height = newHeight;
        }
        return window;
    }


}
