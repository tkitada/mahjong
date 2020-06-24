using Simple.Common.Models;
using System;

namespace Simple.Player.Application
{
    public class TsumoEventArgs : EventArgs
    {
        public TsumoNotification Tsumo{ get; set; }

        public TsumoEventArgs(TsumoNotification tsumo)
        {
            Tsumo = tsumo;
        }
    }
}