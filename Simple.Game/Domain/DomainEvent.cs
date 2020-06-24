using Simple.Common.Models;
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

    internal class HandNotificationEventArgs : EventArgs
    {
        public HandNotification Hand { get; }

        public HandNotificationEventArgs(HandNotification hand)
        {
            Hand = hand;
        }
    }

    internal class TsumoNotificationEventArgs : EventArgs
    {
        public TsumoNotification Tsumo { get; }

        public TsumoNotificationEventArgs(TsumoNotification tsumo)
        {
            Tsumo = tsumo;
        }
    }
}