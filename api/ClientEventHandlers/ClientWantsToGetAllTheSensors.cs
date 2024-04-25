using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToGetAllTheSensorsDto : BaseDto
    {

    }
    public class ClientWantsToGetAllTheSensors(SensorService sensorService) : BaseEventHandler<ClientWantsToGetAllTheSensorsDto>
    {
        public override Task Handle(ClientWantsToGetAllTheSensorsDto dto, IWebSocketConnection socket)
        {
            IEnumerable<Sensor> allSensors = sensorService.GetAllSensors();

            if(allSensors.Any())
            {
                var messageFromServer = new ServerReturnsAllSensors
                {
                    MessageBack = "Successfully retrieved all the sensors!",
                    AllSensors = allSensors
                };
                socket.Send(JsonSerializer.Serialize(messageFromServer));
            }
            else
            {
                var messageFromServer = new ServerReturnsAllSensors
                {
                    MessageBack = "There is either no sensors in the database or idk!",
                    AllSensors = allSensors
                };

                socket.Send(JsonSerializer.Serialize(messageFromServer));
            }

            return Task.CompletedTask;
        }
    }

    public class ServerReturnsAllSensors : BaseDto
    {
        public string? MessageBack { get; set; }
        public IEnumerable<Sensor>? AllSensors { get; set; }
        
    }


}