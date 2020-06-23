using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Player.Application
{
    public class TsumoEventArgs : EventArgs
    {
        public TileIds Hand { get; set; }
        public TileId TsumoTile { get; set; }
    }
}
