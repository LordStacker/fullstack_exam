using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToDeleteDeviceDto : BaseDto     
    {
        public int DeviceId { get; set; }
    }

    public class ClientWantsToDeleteDevice(DeviceService deviceService) : BaseEventHandler<ClientWantsToDeleteDeviceDto>
    {
        public override Task Handle(ClientWantsToDeleteDeviceDto dto, IWebSocketConnection socket)
        {
            deviceService.DeleteDevice(dto.DeviceId);
            return Task.CompletedTask;                      
        }
    }


}