using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IMqtt.Applicazione.View.SettingsPages;

namespace IMqtt.Applicazione.Code.ViewModel;

public partial class SettingsViewModel : ObservableObject
{


    [RelayCommand]
    public async void GotoServerSettings()
    {
        await App.Current.MainPage.Navigation.PushAsync(new ServerSettingPage());
    }
}