using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWatnsToCreateUserToDeviceDto : BaseDto
    {
        public int UserId { get; set; }
        public int DeviceId { get; set; }
    }
    public class ClientWatnsToCreateUserToDevice(UserToDeviceService userToDeviceService) : BaseEventHandler<ClientWatnsToCreateUserToDeviceDto>
    {
        public override Task Handle(ClientWatnsToCreateUserToDeviceDto dto, IWebSocketConnection socket)
        {
            if(dto.UserId != 0 && dto.DeviceId != 0)
            {
                userToDeviceService.CreateUserToDevice(dto.UserId, dto.DeviceId);
            }
            return Task.CompletedTask;
        }
    }


}