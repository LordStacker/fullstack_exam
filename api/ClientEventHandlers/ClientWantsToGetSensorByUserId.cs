using System.Text.Json;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToGetSensorByUserIdDto : BaseDto
    {
        public int UserId { get; set; }
    }
    public class ClientWantsToGetSensorByUserId(SensorService sensorService) : BaseEventHandler<ClientWantsToGetSensorByUserIdDto>
    {
        public override async Task Handle(ClientWantsToGetSensorByUserIdDto dto, IWebSocketConnection socket)
        {
            var userId = dto.UserId;
            Sensor sensor = null;
            try
            {
                sensor = sensorService.GetSensorByUserId(userId);
            }
            catch (Exception e)
            {
                await socket.Send(JsonSerializer.Serialize(new {
                    eventType = "SensorReadFailed",
                    message = "No sensor data found for the specified user"
                }));
                return; 
            }

            if (sensor != null)
            {
                await socket.Send(JsonSerializer.Serialize(new {
                    eventType = "SensorReadRetrieved",
                    Sensor = sensor
                }));
            }
            else
            {
                await socket.Send(JsonSerializer.Serialize(new {
                    eventType = "SensorReadFailed",
                    message = "No sensor data found for the specified user."
                }));
            }
        }
    }


}