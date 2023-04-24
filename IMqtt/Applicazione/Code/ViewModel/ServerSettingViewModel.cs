using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IMqtt.Applicazione.Code.ViewModel;

public partial class ServerSettingViewModel : ObservableObject
{

    [RelayCommand]
    public async void GoBack()
    {
        await App.Current.MainPage.Navigation.PopAsync();
    }

}