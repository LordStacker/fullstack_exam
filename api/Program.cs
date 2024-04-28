using System.Reflection;
using api;
using Fleck;
using fs_exam;
using lib;

var builder = WebApplication.CreateBuilder(args);

var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

builder.Services.AddSingeltons();



var app = builder.Build();
var mqttClientService = app.Services.GetRequiredService<MqttClientService>();
await mqttClientService.ConnectToBrokerAsync();
var server = new WebSocketServer("ws://0.0.0.0:8181");

var wsConnections = new List<IWebSocketConnection>();
server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        StateService.AddConnection(ws);
    };
    ws.OnMessage =  async message =>
    {
        //TODO check all the event we gonna build
        try
        {
            await app.InvokeClientEventHandler(clientEventHandlers, ws, message);
        }
        catch (Exception e)
        {
            //TODO exceptions according to events
            Console.WriteLine(e.Message);
        }
        
    };
});
Console.ReadLine();


