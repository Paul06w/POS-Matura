using System;
using System.Text.Json;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace Labordiagnostik
{
    internal class MQTTReadingStrategy : IReadingStrategy
    {
        public async void Read(ObservableList<Well> wells)
        {
            var dialog = new MQTTSelectionWindow();

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                await this.Connect_Client(wells, dialog.Selection);
            }
        }

        private async Task Connect_Client(ObservableList<Well> wells, MQTT selected)
        {
            var mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("172.20.20.222", 1883)
                .Build();
            
            await mqttClient.ConnectAsync(options);

            switch(selected)
            {
                case MQTT.Entire:
                    await mqttClient.SubscribeAsync("wellData");
                    mqttClient.ApplicationMessageReceivedAsync += async e => { ReceiveEntireMessage(e, wells); };
                    break;

                case MQTT.Single:
                    await mqttClient.SubscribeAsync("wellDataSingle");
                    mqttClient.ApplicationMessageReceivedAsync += async e => { ReceiveSingleMessage(e, wells); };
                    break;
            }

        }

        private async void ReceiveEntireMessage(MqttApplicationMessageReceivedEventArgs e, ObservableList<Well> wells)
        {
            string json = e.ApplicationMessage.ConvertPayloadToString();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            wells.Clear();
            wells.AddRange(JsonSerializer.Deserialize<ObservableList<Well>>(json, options));
        }

        private async void ReceiveSingleMessage(MqttApplicationMessageReceivedEventArgs e, ObservableList<Well> wells)
        {
            string json = e.ApplicationMessage.ConvertPayloadToString();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            //wells.Clear();
            Well well = JsonSerializer.Deserialize<Well>(json, options);

            Well tmp = wells.FirstOrDefault(x => x.WellName == well.WellName);
            if(tmp is null)
            {
                wells.Add(well);
            }
            else if (tmp.WellName == well.WellName)
            {
                wells.Remove(tmp);
                wells.Add(well);
            }
            else
            {
                wells.Add(well);
            }
        }
    }
}
