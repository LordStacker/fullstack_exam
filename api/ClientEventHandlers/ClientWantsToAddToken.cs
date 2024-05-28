using System.Text.Json;
using Fleck;
using fs_exam;
using lib;
using repository.Models;
using service;

namespace fs_exam;

public class ClientWantsToAddTokenDto : BaseDto
{
    public int? UserId { get; set; }
    public string? Token { get; set; }
}

public class ClientWantsToAddToken(UserService userService) : BaseEventHandler<ClientWantsToAddTokenDto>
{
    public override async Task Handle(ClientWantsToAddTokenDto dto, IWebSocketConnection socket) 
    {
        if (dto.UserId == null || string.IsNullOrEmpty(dto.Token))
        {
            await socket.Send(JsonSerializer.Serialize(new { Error = "UserId and Token cannot be empty.", eventType = "ServerError" }));
            return;
        }
        
        userService.UpsertToken(dto.UserId, dto.Token);
        await socket.Send(JsonSerializer.Serialize(new { Message = "Upsert Successful", user_id = dto.UserId, fcm_token = dto.Token, eventType = "ServerConfirmsTokenUpsert"}));

        }
}