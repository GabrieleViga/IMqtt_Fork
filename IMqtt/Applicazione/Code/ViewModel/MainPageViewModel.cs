using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IMqtt.Applicazione.Code.Base;
using IMqtt.Applicazione.Code.DashBoardItems;
using IMqtt.Applicazione.Code.Extension;
using MQTTnet.Client;
namespace IMqtt.Applicazione.Code.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    private StackLayout _stackLayout;
    private MqttServices _mqttServices = App.Service;
    private IMqttClient _client;

    private int _attempts = 10;

    public ObservableCollection<BaseItem> DashBoardItems { get; set; }

    private Timer _timer;

    [ObservableProperty] 
    public bool isConnected = false;
    
    public MainPageViewModel(StackLayout stackLayout)
    {
        _stackLayout = stackLayout;
        Task.Run(async () => await Init()).Wait();

        DashBoardItems = new ObservableCollection<BaseItem>()
        {
            new LabelItem("alex/test"),
            new EntryItem("alex/invio"),
            new MultiLineChart("alex/test")
        };
    }

    private async Task Init()
    {
        await _mqttServices.Setup("test.mosquitto.org", 1883);
        _client = _mqttServices.Client;

        IsConnected = _client.IsConnected;

        _timer = new Timer(async _ =>
            {
                if (await _client.TryPingAsync()) return;

                await _client.TryReConnectAsync();
                IsConnected = _client.IsConnected;

                if (IsConnected)
                    foreach (var item in DashBoardItems) item.SubScribe();

                // if (!IsConnected) _attempts--;
            },
            null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

        }
    
    private async void AddComponent()
    {
        _stackLayout.Children.Add(new LabelItem("alex/test").View);
    }

    [RelayCommand]
    private async void SetTopic(BaseItem item)
    {
        string result = await App.Current.MainPage.DisplayPromptAsync("Impostazioni", "Imposta il topic", placeholder:"alex/invio");
        item.TopicFilter.Topic = result;
    }
}