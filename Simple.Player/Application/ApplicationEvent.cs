using Simple.Common.Models;
using System;

namespace Simple.Player.Application
{
    public class TsumoEventArgs : EventArgs
    {
        public TsumoModel Tsumo{ get; set; }

        public TsumoEventArgs(TsumoModel tsumo)
        {
            Tsumo = tsumo;
        }
    }
}