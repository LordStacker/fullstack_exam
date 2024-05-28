using System.Text.Json;
using Fleck;
using lib;

namespace fs_exam;

public class ClientWantsToEnterRoomDto : BaseDto
{
    public int roomId { get; set; }
}

public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
{
    public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        StateService.AddToRoom(socket, dto.roomId);
        socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
        {
            message = "you were succesfully added to the room: " + dto.roomId
        }));
    return Task.CompletedTask;
    }
}

public class ServerAddsClientToRoom : BaseDto
{
    public string? message { get; set; }
}