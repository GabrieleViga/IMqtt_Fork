using IMqtt.Applicazione.Code.Extension;
using MQTTnet;
using MQTTnet.Channel;
using MQTTnet.Client;
namespace IMqtt.Applicazione.Code;

public class MqttServices
{
    public IMqttClient Client { get; set; }
    private MqttClientOptions _options;

    public async Task Setup(string address, int port)
    {
        var mqttFactory = new MqttFactory();
        Client = mqttFactory.CreateMqttClient();
        _options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(address, port)
            .WithCleanSession()
            .Build();
        
        try { await Client.ConnectAsync(_options); } 
        catch (Exception ex) {}
    }

    public async Task TryConnectAsync()
    {
        await Client.TryConnectAsync(_options);
    }
}