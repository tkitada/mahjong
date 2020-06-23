using System;

namespace Simple.Player.Domain
{
    internal class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}