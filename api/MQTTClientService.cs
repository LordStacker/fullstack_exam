
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
                    .WithTcpServer("mqtt.flespi.io", 1883)
                    .WithCredentials(Environment.GetEnvironmentVariable("flespiconn"), "")
                    .WithProtocolVersion(MqttProtocolVersion.V500)
                    .Build();
                
                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                Console.WriteLine("Connected to MQTT broker successfully.");
                
                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f => f.WithTopic(Environment.GetEnvironmentVariable("topic")))
                    .Build();
                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    Console.WriteLine(e.ApplicationMessage.ConvertPayloadToString());
                };

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