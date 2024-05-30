using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Fleck;
using fs_exam;
using lib;
using repository.Models;
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
        public override async Task Handle(ClientWantsToRegisterDto dto, IWebSocketConnection socket)
        {
            User currentUser = null;
            

            if(userService.CheckIfUsernameExists(dto.Username!).Username == dto.Username)
            {
                //throw new ValidationException($"User with {dto.Username} username already exists!");
                await socket.Send(JsonSerializer.Serialize(new { Message = "User already exist", eventType = "FailedRegisterUserExist" }));
                
            }
            else if (dto.Username != null && dto.Email != null && dto.Password != null)
            { 
                currentUser =  userService.CreateUser(dto.Username, dto.Email, dto.Password);
                StateService.Connections[socket.ConnectionInfo.Id].User = currentUser;
                await socket.Send(JsonSerializer.Serialize(new { Message = "User creation success: "+ dto.Username, eventType = "UserCreatedSuccessfully" }));
            }
        }
    }

    public class ServerReturnsRegisterInfo : BaseDto
    {
        public string? MessageBack { get; set; }     

    }


}