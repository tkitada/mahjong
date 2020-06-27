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

    public class TsumoEventArgs : EventArgs
    {
        public TsumoRes TsumoRes { get; set; }
    }
}