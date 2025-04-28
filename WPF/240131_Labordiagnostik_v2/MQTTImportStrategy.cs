using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using MQTTnet;
using MQTTnet.Client;

namespace _240131_Labordiagnostik
{

    class MQTTImportStrategy : IImportStrategy
    {
        string ip;
        string port;
        string wellinfo;

        public MQTTImportStrategy(string ip, string port, string wellinfo)
        {
            this.ip = ip;
            this.port = port;
            this.wellinfo = wellinfo;
        }

        public MQTTImportStrategy() { }

        public async Task<List<Messwert>> ImportData()
        {
            //string[] socket = txtSocket.Text.Split(':');
            //string topic = (string)((ComboBoxItem)cmbTopic.SelectedItem).Content;
            List<Messwert> list = new List<Messwert>();

            // Create a MQTT client factory
            MqttFactory factory = new();

            // Create a MQTT client instance
            IMqttClient mqttClient = factory.CreateMqttClient();

            // Create MQTT client options
            MqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(ip, Convert.ToInt32(port))
                .WithCleanSession()
                .Build();

            // Connect to MQTT broker
            MqttClientConnectResult connectResult = await mqttClient.ConnectAsync(options);

            if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
            {
                Trace.WriteLine("Connected to MQTT broker successfully.");

                // Subscribe to a topic
                await mqttClient.SubscribeAsync(wellinfo);

                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Trace.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
                    list = JsonSerializer.Deserialize<List<Messwert>>(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));            //wird in die Liste deserialisiert

                    return Task.CompletedTask;
                };


                // Unsubscribe and disconnect
                await mqttClient.UnsubscribeAsync(wellinfo);
                await mqttClient.DisconnectAsync();
                Trace.WriteLine("Disconnected from MQTT");
            }
            else
            {
                Trace.WriteLine($"Failed to connect to MQTT broker: {connectResult.ResultCode}");

            }
        }
    }
}
