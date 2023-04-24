using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMqtt.Applicazione.View.SettingsPages;

public partial class ServerSettingPage : ContentPage
{
    public ServerSettingPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this,false);
    }
}