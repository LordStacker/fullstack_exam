using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Fleck;
using fs_exam;
using lib;
using service;

namespace api.ClientEventHandlers
{
    public class ClientWantsToRegisterDto : BaseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class ClientWantsToRegister(UserService userService) : BaseEventHandler<ClientWantsToRegisterDto>
    {
        public override Task Handle(ClientWantsToRegisterDto dto, IWebSocketConnection socket)
        {
            if(userService.CheckIfUsernameExists(dto.Username!).Username == dto.Username)
            {
                var messageFromServer = new ServerReturnsRegisterInfo 
                {
                    MessageBack = $"User with username {dto.Username} already exists!"
                };
                socket.Send(JsonSerializer.Serialize(messageFromServer));
                throw new ValidationException($"User with {dto.Username} username already exists!");
            }
            else if(dto.Username != null && dto.Email != null && dto.Password != null)
            {
                var currentUser =  userService.CreateUser(dto.Username, dto.Email, dto.Password);

                var messageFromServer = new ServerReturnsRegisterInfo 
                {
                    MessageBack = $"User with username {dto.Username} and email {dto.Email} is created successfully!"
                };
                
                socket.Send(JsonSerializer.Serialize(messageFromServer));
                StateService.Connections[socket.ConnectionInfo.Id].User = currentUser;
            }

            return Task.CompletedTask;

        }
    }

    public class ServerReturnsRegisterInfo : BaseDto
    {
        public string? MessageBack { get; set; }     

    }


}