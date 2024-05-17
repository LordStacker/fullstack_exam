using System.Text.Json;
using Fleck;
using lib;
using repository.Models;
using service;

namespace fs_exam;

public class ClientWantsToSignInDto : BaseDto
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class ClientWantsToSignIn(UserService userService) : BaseEventHandler<ClientWantsToSignInDto>
{
    public override async Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
        {
            await socket.Send(JsonSerializer.Serialize(new { Error = "Username and password cannot be empty.", eventType = "ServerError" }));
            return;
        }

        User currentUser = null;
        try
        {
            currentUser = userService.ValidateUser(dto.Username, dto.Password);
        }
        catch (Exception e)
        {
            await socket.Send(JsonSerializer.Serialize(new { Message = "Failed to log in credentials wrong", eventType = "WrongCredentialsEvent" }));
        }
        
        StateService.Connections[socket.ConnectionInfo.Id].User = currentUser;
        await socket.Send(JsonSerializer.Serialize(new { Message = "SignInSuccessful", Username = currentUser.Username, user_id = currentUser.Id, eventType = "ServerConfirmsSignIn"}));
    }
}