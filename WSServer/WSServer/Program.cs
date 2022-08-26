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

    class Program
    {
        static void Main(string[] args)
        {
            // WS server instance
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:9090");

            // WS Service Route with Echo WS Behavior
            wssv.AddWebSocketService<Echo>("/Echo");

            // Start Server
            wssv.Start();

            Console.WriteLine("Server: started on ws://127.0.0.1:9090/Echo");

            // Stop server on key pressed
            Console.ReadKey();
            wssv.Stop();
        }
    }
}
