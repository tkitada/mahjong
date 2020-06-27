using Simple.Common.Models;
using System;

namespace Simple.Game.Application
{
    public class JoinEventArgs : EventArgs
    {
        public JoinReq JoinReq { get; }

        public JoinEventArgs(JoinReq joinReq)
        {
            JoinReq = joinReq;
        }
    }
}