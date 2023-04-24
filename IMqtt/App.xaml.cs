using IMqtt.Applicazione.Code;
using IMqtt.Applicazione.View;

namespace IMqtt;

public partial class App : Application
{
    public static MqttServices Service { get; set; }
    public App()
    {
        InitializeComponent();
        Service = new MqttServices();
        MainPage = new NavigationPage(new MainTabbedPage());
    }
}