using System.Text.Json;
using Fleck;
using lib;

namespace fs_exam;

public class ClientWantsToBroadcastToRoomDto : BaseDto
{
    public string? message { get; set; }
    public int roomId { get; set; }
}

public class ClientsWantToBroadcastToRoom : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
{
    public override Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
    {
        var message = new ServerBroadcastsMessageWithUser()
        {
            message = dto.message,
            user = StateService.Connections[socket.ConnectionInfo.Id].User!.Username!
        };
        StateService.BroadcastToRoom(dto.roomId, JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }
}

public class ServerBroadcastsMessageWithUser : BaseDto
{
    public string? message { get; set; }
    public string? user { get; set; }
}