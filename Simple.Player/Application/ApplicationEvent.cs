using mjlib.Tiles;
using Simple.Common.Models;
using System;

namespace Simple.Player.Application
{
    public class JoinEventArgs : EventArgs
    {
        public JoinRes JoinRes { get; set; }
    }

    public class HandEventArgs : EventArgs
    {
        public HandRes HandRes { get; set; }
    }
}