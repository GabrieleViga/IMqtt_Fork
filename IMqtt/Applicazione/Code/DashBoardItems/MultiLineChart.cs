using System.Collections.ObjectModel;
using System.Text;
using AlohaKit.Controls;
using AlohaKit.Models;
using IMqtt.Applicazione.Code.Base;
using MQTTnet.Client;

namespace IMqtt.Applicazione.Code.DashBoardItems;

public class MultiLineChart : BaseItem
{
    private ObservableCollection<ChartItem> Items;

    public MultiLineChart(string topic) : base(topic, true)
    {
        Items = new ObservableCollection<ChartItem>()
        {
            new() { Label = DateTime.Now.ToString("hh:mm:ss"), Value = 0}
        };
        View = new LineChart {AxisLinesColor = Colors.Pink, ReanimateOnPropertyChanged = false, DisplayHeaderValues = true, DisplayHorizontalAxisLines = true, DisplayVerticalAxisLines = true, DisplayValueLabelsOnTop = true, PointSize = 0,CurveFactor = 20, Entries = Items, FillColor = Colors.Aqua, WidthRequest = 400, HeightRequest = 300, LineColor = Colors.Green, FooterLabelsFontColor = Colors.Black, FontColor = Colors.Black};
    }

    protected override void OnSubscribeEvent(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        Items.Add(new ChartItem() {
            Label = DateTime.Now.ToString("hh:mm:ss"), 
            Value = int.Parse(Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment)) 
        });
    }

    protected override string GetPublishValue()
    {
        throw new NotImplementedException();
    }
}