using System.Text.Json;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToGetAllDevicesDto : BaseDto
    {
    }
    public class ClientWantsToGetAllDevices(DeviceService deviceService) : BaseEventHandler<ClientWantsToGetAllDevicesDto>
    {
        public override Task Handle(ClientWantsToGetAllDevicesDto dto, IWebSocketConnection socket)
        {
            IEnumerable<Device> allDevices = deviceService.GetAllDevices();
            if(allDevices.Any())
            {
                var messageFromServer = new ServerReturnsAllTheDevices
                {
                    MessageBack = "Successfully retrieved all the devices!",
                    AllDevices = allDevices
                };
                socket.Send(JsonSerializer.Serialize(messageFromServer));
            }
            else
            {
                var messageFromServer = new ServerReturnsAllTheDevices
                {
                    MessageBack = "There is either no devices in the database or idk!",
                    AllDevices = allDevices
                };

                socket.Send(JsonSerializer.Serialize(messageFromServer));
            }

            return Task.CompletedTask;
        }
    }

    public class ServerReturnsAllTheDevices : BaseDto
    {
        public string? MessageBack { get; set; }
        public IEnumerable<Device>? AllDevices { get; set; }
        
    }


}