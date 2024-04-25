using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToUpdateSensorDto : BaseDto
    {
        public int SensorId { get; set; }
        public decimal SoundLevel { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
    }
    public class ClientWantsToUpdateSensor(SensorService sensorService) : BaseEventHandler<ClientWantsToUpdateSensorDto>
    {
        public override Task Handle(ClientWantsToUpdateSensorDto dto, IWebSocketConnection socket)
        {
            if(dto.SensorId == 0 && dto.SoundLevel == 0 && dto.Temperature == 0 && dto.Humidity == 0)
                throw new Exception($"Could not update sensor with id: {dto.SensorId}. There is a missing property maybe, idk!");

            sensorService.UpdateSensor(dto.SensorId, dto.SoundLevel, dto.Temperature, dto.Humidity);

            return Task.CompletedTask;
        }
    }


}