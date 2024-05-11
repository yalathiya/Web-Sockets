using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace WsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // server url 
            builder.WebHost.UseUrls("https://localhost:6969");
            
            var app = builder.Build();
            app.UseWebSockets();

            // accepting web socket request 
            app.Map("/ws", async context => 
            { 
                if(context.WebSockets.IsWebSocketRequest)
                {
                    // using web socket  
                    using (var ws = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        // send message 
                        var message = "Hello from server !!";
                        var bytes = Encoding.UTF8.GetBytes(message);
                        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        await ws.SendAsync(arraySegment,
                                           WebSocketMessageType.Text,
                                           true,
                                           CancellationToken.None);
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            });

            app.RunAsync();
        }
    }
}