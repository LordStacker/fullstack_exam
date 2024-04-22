using System.Text.Json;
using Fleck;
using lib;
namespace fs_exam;

public class ClientWantsToEchoServerDto : BaseDto
{
    public string messageContent { get; set; }
}

public class ClientWantsToEchoServer : BaseEventHandler<ClientWantsToEchoServerDto>
{
    public override Task Handle(ClientWantsToEchoServerDto dto, IWebSocketConnection socket)
    {
        var echo = new ServerEchosClient()
        {
            echoValue = "Echo: " + dto.messageContent
        };
        var messageToClient = JsonSerializer.Serialize(echo);
        socket.Send(messageToClient);
        return Task.CompletedTask;
    }
}

public class ServerEchosClient
{
    public string echoValue { get; set; }
}