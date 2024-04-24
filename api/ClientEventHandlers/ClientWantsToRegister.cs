using System.ComponentModel.DataAnnotations;
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
                throw new ValidationException($"User with {dto.Username} username already exists!");
            }
            else if(dto.Username != null && dto.Email != null && dto.Password != null)
            {
                var currentUser =  userService.CreateUser(dto.Username, dto.Email, dto.Password);
                StateService.Connections[socket.ConnectionInfo.Id].User = currentUser;
            }

            return Task.CompletedTask;

        }
    }


}