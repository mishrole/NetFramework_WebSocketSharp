using System;

namespace WSServer
{
    public class Message
    {
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Room { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
