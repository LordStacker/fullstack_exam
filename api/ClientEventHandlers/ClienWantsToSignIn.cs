using Fleck;
using lib;
using service;

namespace fs_exam;

public class ClientWantsToSignInDto : BaseDto
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class ClientWantsToSignIn(UserService userService) : BaseEventHandler<ClientWantsToSignInDto>
{
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        if (dto.Username != null && dto.Password != null)
        {
            var currentUser = userService.ValidateUser(dto.Username, dto.Password); 
            StateService.Connections[socket.ConnectionInfo.Id].User = currentUser;            
        }
        else
        {
            throw new Exception("Somethings going on!");
        }
        
        return Task.CompletedTask;
    }
}