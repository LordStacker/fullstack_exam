using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToDeleteUserToDeviceDto : BaseDto
    {
        public int UserId { get; set; }
    }
    public class ClientWantsToDeleteUserToDevice(UserToDeviceService userToDeviceService) : BaseEventHandler<ClientWantsToDeleteUserToDeviceDto>
    {
        public override Task Handle(ClientWantsToDeleteUserToDeviceDto dto, IWebSocketConnection socket)
        {
            userToDeviceService.DeleteUserToDevice(dto.UserId);
            return Task.CompletedTask;
        }
    }

    
}