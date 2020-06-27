using System;

namespace Simple.Game.Domain
{
    internal interface IMessageServer
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        void SendMessage(string message);
    }
}