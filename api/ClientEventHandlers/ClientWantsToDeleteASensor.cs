using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToDeleteASensorDto : BaseDto
    {
        public int SensorId { get; set; }
    }
    public class ClientWantsToDeleteASensor(SensorService sensorService) : BaseEventHandler<ClientWantsToDeleteASensorDto>
    {
        public override Task Handle(ClientWantsToDeleteASensorDto dto, IWebSocketConnection socket)
        {
           sensorService.DeleteSensor(dto.SensorId);
           return Task.CompletedTask; 
        }
    }


}