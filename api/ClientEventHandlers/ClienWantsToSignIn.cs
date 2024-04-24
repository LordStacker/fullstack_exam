using Fleck;
using lib;

namespace fs_exam;

public class ClientWantsToSignInDto : BaseDto
{
    public string? User { get; set; }
}

public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
{
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        //StateService.Connections[socket.ConnectionInfo.Id].User = User;
        return Task.CompletedTask;
    }
}