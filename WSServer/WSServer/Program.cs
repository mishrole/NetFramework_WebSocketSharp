using Newtonsoft.Json;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WSServer
{
    // Custome implementation of WSBehavior
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Server: Received message from client is -> '" + e.Data + "'");
            // Send message to the server
            Send("Hi, I'm a server. I received your message '" + e.Data + "'");
        }
    }

    public class EchoAll : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Server: Received message from client is -> '" + e.Data + "'");
            // Send message to all sessions
            var message = new Message
            {
                Content = "Received message from client",
                CreatedAt = DateTime.Now,
                Room = "/EchoAll",
                Sender = "Server",
            };

            //Sessions.Broadcast("Hi, I'm a server. I received your message '" + e.Data + "'");
            Sessions.Broadcast(JsonConvert.SerializeObject(message));

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // WS server instance
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:9090");

            // WS Service Route with Echos WS Behavior
            wssv.AddWebSocketService<Echo>("/Echo");
            wssv.AddWebSocketService<EchoAll>("/EchoAll");

            // Start Server
            wssv.Start();

            Console.WriteLine("Server: started on ws://127.0.0.1:9090");
            Console.WriteLine("Use /Echo to send and receive from 1 client");
            Console.WriteLine("Use /EchoAll to send and receive from many clients");

            // Stop server on key pressed
            Console.ReadKey();
            wssv.Stop();
        }
    }
}
