
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using MQTTnet.Formatter;
using Newtonsoft.Json;
using repository.Models;
using service;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace api
{
    public class MqttClientService(SensorService sensorService)
    {
        private IMqttClient mqttClient;

        
        private SensorData? _sensorData;
        
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
                    await SendMessageSomewhereElseAsync(JsonConvert.DeserializeObject<SensorData>(e.ApplicationMessage.ConvertPayloadToString()) ?? throw new InvalidCastException("Could not deserialize"));
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
                throw;
            }
        }
        private async Task SendMessageSomewhereElseAsync(SensorData sensorData)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(sensorData));
            int deviceId = sensorData.device_id;
            decimal soundLevel = sensorData.sound_level;
            int temperature = (int)sensorData.temperature;
            int humidity = (int)sensorData.humidity;
            DateTime date = DateTime.Now;

            Console.WriteLine(deviceId);
            Console.WriteLine(soundLevel);
            Console.WriteLine(temperature);
            Console.WriteLine(humidity);
            Console.WriteLine(date);
            try
            {
                var returned = sensorService.CreateSensor(deviceId, soundLevel, temperature, humidity, date);
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(returned));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);
            }

            
        }
    }
}