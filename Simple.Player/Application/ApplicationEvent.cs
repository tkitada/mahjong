using Simple.Common.Models;
using System;

namespace Simple.Player.Application
{
    public class JoinEventArgs : EventArgs
    {
        public JoinRes JoinRes { get; }

        public JoinEventArgs(JoinRes joinRes)
        {
            JoinRes = joinRes;
        }
    }
}