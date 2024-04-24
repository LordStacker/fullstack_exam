using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToCreateDeviceDto : BaseDto
    {
        public string? DeviceName { get; set; }
        public int UserId { get; set; }
    }
    public class ClientWantsToCreateDevice(DeviceService deviceService) : BaseEventHandler<ClientWantsToCreateDeviceDto>
    {
        public override Task Handle(ClientWantsToCreateDeviceDto dto, IWebSocketConnection socket)
        {
            if( dto.DeviceName != null )
            {
                deviceService.CreateDevice(dto.DeviceName, dto.UserId);
            }

            return Task.CompletedTask;
        }
    }
}