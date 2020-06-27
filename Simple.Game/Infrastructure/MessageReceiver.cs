using Simple.Game.Domain;
using System;

namespace Simple.Game.Infrastructure
{
    internal class MessageReceiver : IMessageReceiver
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;
        public MessageReceiver(IMessageServer server)
        {
            server.MessageReceivedEvent += (_, e) => MessageReceivedEvent(this, e);
        }
    }
}