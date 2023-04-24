using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;

namespace IMqtt.Applicazione.Code.Base;

public abstract class BaseItem
{
    private static List<MqttTopicFilter> TopicList = new List<MqttTopicFilter>();
    public MqttTopicFilter TopicFilter { get; set; }
    protected IMqttClient client;
    protected ICommand OnPublishCommand;

    public bool isSubscribe;
    public IView View { get; set; }

    protected BaseItem(string topic, bool isSubscribe)
    {
        client = App.Service.Client;
        this.isSubscribe = isSubscribe;
        
        if (client == null) return; //Inutile
        
        this.TopicFilter = new MqttTopicFilterBuilder()
            .WithTopic(topic)
            .Build();
        
        TopicList.Add(TopicFilter);
        
        if (isSubscribe) client.SubscribeAsync(TopicFilter);

        client.ApplicationMessageReceivedAsync += OnSubscribe; 
        OnPublishCommand = new Command(OnPublish);
    }

    public void SubScribe()
    {
        if (isSubscribe) client.SubscribeAsync(TopicFilter);

        client.ApplicationMessageReceivedAsync += OnSubscribe; 
        OnPublishCommand = new Command(OnPublish);
    }

    private async Task OnSubscribe(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        if (!client.IsConnected) return;
        if (!eventArgs.ApplicationMessage.Topic.Equals(TopicFilter.Topic)) return;
        await Device.InvokeOnMainThreadAsync(() => OnSubscribeEvent(eventArgs));
    }

    protected abstract void OnSubscribeEvent(MqttApplicationMessageReceivedEventArgs eventArgs);
    protected abstract string GetPublishValue();
    
    protected async void OnPublish()
    {
        if (!client.IsConnected) return;

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(TopicFilter.Topic)
            .WithPayload(GetPublishValue())
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();
        
        await client.PublishAsync(message);
    }
}