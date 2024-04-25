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
    public class ClientWantsToGetDeviceByIdDto : BaseDto
    {
        public int DeviceId { get; set; }
    }
    public class ClientWantsToGetDeviceById(DeviceService deviceService) : BaseEventHandler<ClientWantsToGetDeviceByIdDto>
    {
        public override Task Handle(ClientWantsToGetDeviceByIdDto dto, IWebSocketConnection socket)
        {
            if(dto.DeviceId != 0)
            {
               var device = deviceService.GetDevice(dto.DeviceId);
                if(device != null)
                {
                    var messageFromServer = new ServerReturnsADevice
                    {
                        MessageBack = "Here is the wanted device!",
                        Device = device
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer));
                }
                else
                {
                     var messageFromServer = new ServerReturnsADevice
                    {
                        MessageBack = "There is something wrong idk :(",
                        Device = device
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer));
                }
            }
            
            return Task.CompletedTask;
        }
    }

    public class ServerReturnsADevice : BaseDto
    {
        public string? MessageBack { get; set; }
        public Device? Device { get; set; }
        
    }


}