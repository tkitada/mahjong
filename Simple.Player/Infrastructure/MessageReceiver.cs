using Simple.Player.Domain;
using System;

namespace Simple.Player.Infrastructure
{
    internal class MessageReceiver : IMessageReceiver
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        public MessageReceiver(IMessageClient client)
        {
            client.MessageReceivedEvent += (_, e) => MessageReceivedEvent(this, e);
        }
    }
}