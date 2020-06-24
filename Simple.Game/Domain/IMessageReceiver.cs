using System;

namespace Simple.Game.Domain
{
    internal interface IMessageReceiver
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;
    }
}