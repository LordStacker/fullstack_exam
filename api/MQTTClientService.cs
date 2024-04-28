
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using MQTTnet.Formatter;

namespace api
{
    public class MqttClientService
    {
        private IMqttClient mqttClient;

        public async Task<bool> ConnectToBrokerAsync()
        {
            try
            {
                var mqttFactory = new MqttFactory();
                mqttClient = mqttFactory.CreateMqttClient();

                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("mqtt.flespi.io", 443)
                    .WithClientId("mqtt-board-panel-322475a2")
                    .WithCredentials("KtuM5fQVb7jbK5yEChfw9qWbaEaVBSjflO5IfgAaTVOocuwrSCMnVZeuU22bjA5e", "")
                    .WithProtocolVersion(MqttProtocolVersion.V311)
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                Console.WriteLine("Connected to MQTT broker successfully.");
                return true;
            }
            catch (MqttCommunicationException ex)
            {
                Console.WriteLine($"Error connecting to MQTT broker: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }
    }
}