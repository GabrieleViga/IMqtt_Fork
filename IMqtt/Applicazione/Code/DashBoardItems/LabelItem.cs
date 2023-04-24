using System.Text;
using IMqtt.Applicazione.Code.Base;
using MQTTnet.Client;

namespace IMqtt.Applicazione.Code.DashBoardItems;

public class LabelItem : BaseItem
{
    public LabelItem(string topic) : base(topic, true)
    {
        View = new Label { Text = "0", TextColor = Colors.Black, BackgroundColor = Colors.White};
    }

    protected override void OnSubscribeEvent(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        ((Label)View).Text = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);
    }

    protected override string GetPublishValue()
    {
        throw new NotImplementedException();
    }
}