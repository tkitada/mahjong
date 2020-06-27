using System;

namespace Simple.Game.Domain
{
    internal class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }

    internal class GameInfoNotificationEventArgs : EventArgs
    {
        public GameInformation GameInfo { get; }

        public GameInfoNotificationEventArgs(GameInformation gameInfo)
        {
            GameInfo = gameInfo;
        }
    }

}