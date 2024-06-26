using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToCreateSensorDto : BaseDto
    {
        public int DeviceId { get; set; }
        public decimal SoundLevel { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        
    }
    public class ClientWantsToCreateSensor(SensorService sensorService) : BaseEventHandler<ClientWantsToCreateSensorDto>
    {
        public override Task Handle(ClientWantsToCreateSensorDto dto, IWebSocketConnection socket)
        {
            if(dto.DeviceId == 0 && dto.SoundLevel == 0 && dto.Temperature == 0 && dto.Humidity == 0)
                throw new Exception("There is a missing property! Check it out or idk :)");

            sensorService.CreateSensor(dto.DeviceId, dto.SoundLevel, (int)dto.Temperature, (int)dto.Humidity, DateTime.Now);
            
            return Task.CompletedTask;
            
        }
    }


}