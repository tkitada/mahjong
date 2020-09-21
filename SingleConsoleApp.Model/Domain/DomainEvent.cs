using mjlib.HandCalculating;
using System;

namespace SingleConsoleApp.Model.Domain
{
    internal class AgariEventArgs : EventArgs
    {
        public HandResponse Result { get; set; }
    }
}