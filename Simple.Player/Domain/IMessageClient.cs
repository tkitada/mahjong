using System;

namespace Simple.Player.Domain
{
    internal interface IMessageClient
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        void SendMessage(string message);
    }
}