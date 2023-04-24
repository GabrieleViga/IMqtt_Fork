using System.Text;
using System.Windows.Input;
using IMqtt.Applicazione.Code.Base;
using MQTTnet.Client;

namespace IMqtt.Applicazione.Code.DashBoardItems;

public class EntryItem : BaseItem
{
    public EntryItem(string topic) : base(topic, false)
    {
        View = new Entry { Placeholder = "Inserisci il testo" , TextColor = Colors.Black, BackgroundColor = Colors.White, ReturnCommand = OnPublishCommand };
    }

    protected override void OnSubscribeEvent(MqttApplicationMessageReceivedEventArgs eventArgs) { }
    protected override string GetPublishValue() => ((Entry)View).Text;
}