using System.Text.Json;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToGetSensorByIdDto : BaseDto
    {
        public int SensorId { get; set; }
    }
    public class ClientWantsToGetSensorById(SensorService sensorService) : BaseEventHandler<ClientWantsToGetSensorByIdDto>
    {
        public override Task Handle(ClientWantsToGetSensorByIdDto dto, IWebSocketConnection socket)
        {
           if(dto.SensorId != 0)
            {
               var sensor = sensorService.GetSensorById(dto.SensorId);
                if(sensor != null)
                {
                    var messageFromServer = new ServerReturnsASensor
                    {
                        MessageBack = "Here is the wanted sensor info!",
                        Sensor = sensor
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer));
                }
                else
                {
                     var messageFromServer = new ServerReturnsASensor
                    {
                        MessageBack = "There is something wrong idk :(",
                        Sensor = sensor
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer));
                }
            }
            
            return Task.CompletedTask;
        }
    }

    public class ServerReturnsASensor : BaseDto
    {
        public string? MessageBack { get; set; }
        public Sensor? Sensor { get; set; }
        
    }

}