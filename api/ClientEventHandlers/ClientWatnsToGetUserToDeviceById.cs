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
    public class ClientWatnsToGetUserToDeviceByIdDto : BaseDto
    {
        public int UserId { get; set; }
    }
    public class ClientWatnsToGetUserToDeviceById(UserToDeviceService userToDeviceService) : BaseEventHandler<ClientWatnsToGetUserToDeviceByIdDto>
    {
        public override Task Handle(ClientWatnsToGetUserToDeviceByIdDto dto, IWebSocketConnection socket)
        {
            if(dto.UserId != 0)
            {
                var userToDevice = userToDeviceService.GetUserToDevice(dto.UserId);
                if(userToDevice != null)
                {
                    var messageFromServer = new ServerReturnsAUserToDevice
                    {
                        MessageBack = "Here is the wantetd user to device",
                        UserToDevice = userToDevice
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer));

                }
                else
                {
                   var messageFromServer = new ServerReturnsAUserToDevice
                    {
                        MessageBack = $"Something is wrong. Could not get the user to device with id: {dto.UserId}!",
                        UserToDevice = userToDevice
                    };
                    socket.Send(JsonSerializer.Serialize(messageFromServer)); 
                }
            }
            return Task.CompletedTask;
        }
    }

    public class ServerReturnsAUserToDevice : BaseDto
    {
        public string? MessageBack { get; set; }
        public UserToDevice? UserToDevice { get; set; }
        
    }


}