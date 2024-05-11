using System.Net.WebSockets;
using System.Text;

namespace WsClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var ws = new ClientWebSocket();

            Console.WriteLine("Connecting to server");
            await ws.ConnectAsync(new Uri("wss://localhost:44383/ws"), CancellationToken.None);
            Console.WriteLine("Connected !");

            var recieveTask = Task.Run(async () =>
            {
                var buffer = new byte[1024*4];    
                while(true)
                {
                    var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    
                    if(result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received: " + message); 
                }
            });

            await recieveTask;
        }
    }
}