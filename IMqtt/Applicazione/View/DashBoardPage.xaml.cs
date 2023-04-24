using IMqtt.Applicazione.Code.ViewModel;

namespace IMqtt.Applicazione.View;

public partial class DashBoardPage : ContentPage
{
    public DashBoardPage()
    { 
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this,false);
        BindingContext = new MainPageViewModel(StackLayout);
    }
}