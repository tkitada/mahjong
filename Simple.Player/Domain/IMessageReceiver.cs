﻿using System;

namespace Simple.Player.Domain
{
    internal interface IMessageReceiver
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;
    }
}