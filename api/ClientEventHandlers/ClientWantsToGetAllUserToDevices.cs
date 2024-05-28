using System.Text.Json;
using Fleck;
using lib;
using repository.Models;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToGetAllUserToDevicesDto : BaseDto
    {

    }
    public class ClientWantsToGetAllUserToDevices(UserToDeviceService userToDeviceService) : BaseEventHandler<ClientWantsToGetAllUserToDevicesDto>
    {
        public override Task Handle(ClientWantsToGetAllUserToDevicesDto dto, IWebSocketConnection socket)
        {
            IEnumerable<UserToDevice> allUserToDevice = userToDeviceService.GetAllUserToDevice();

            if( allUserToDevice.Any())
            {
                var messageFromServer = new ServerReturnsAllTheUserToDevices
                {
                    MessageBack = "Succesfully retreived all user to device",
                    AllUserToDevices = allUserToDevice
                };
                socket.Send(JsonSerializer.Serialize(messageFromServer));

            }
            else
            {
                var messageFromServer = new ServerReturnsAllTheUserToDevices
                {
                    MessageBack = "There is a problem with retriving this data!",
                    AllUserToDevices = allUserToDevice
                };
                socket.Send(JsonSerializer.Serialize(messageFromServer));
            }
            return Task.CompletedTask;
        }
    }

    public class ServerReturnsAllTheUserToDevices : BaseDto
    {
        public string? MessageBack { get; set; }
        public IEnumerable<UserToDevice>? AllUserToDevices { get; set; }
        
    }

}