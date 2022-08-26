using Newtonsoft.Json;
using System;

using WebSocketSharp;

namespace WSClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // WS client instance
            using (WebSocket wsc = new WebSocket("ws://127.0.0.1:9090/EchoAll"))
            {
                // Add Handler to listen
                wsc.OnMessage += WS_AnswerToEmitter;
                // Connect to server
                wsc.Connect();
                // Send data to Server
                wsc.Send("Hello WS Server!");
                Console.WriteLine("Client: 'Hello WS Server' sended to Server");

                // Close console on key pressed
                Console.ReadKey();
            }

        }

        private static void WS_AnswerToEmitter(object sender, MessageEventArgs e)
        {
            //Console.WriteLine("Client: Message received from server is -> '" + e.Data + "'");
            try
            {
                Message message = JsonConvert.DeserializeObject<Message>(e.Data);
                Console.WriteLine("Created a message: '" + message.Content + "' at " + message.CreatedAt + " from " +  message.Sender + " " + message.Room);
            } catch (Exception)
            {
                Console.WriteLine("Client: Hmm, I don't understand " + e.Data);
            }

        }
    }
}
