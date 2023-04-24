using MQTTnet.Client;

namespace IMqtt.Applicazione.Code.Extension;

public static class MqttExtension
{
    public static async Task<bool> TryConnectAsync(this IMqttClient client, MqttClientOptions options)
    {
        try
        {
            await client.ConnectAsync(options);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public static async Task<bool> TryReConnectAsync(this IMqttClient client)
    {
        try
        {
            await client.ReconnectAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}