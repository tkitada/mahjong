using mjlib;
using mjlib.Tiles;
using System.Collections.Generic;

namespace Simple.Common.Models
{
    public class JoinRes
    {
        public int Id { get; set; }
    }

    public class HandRes
    {
        public TileIds Hand { get; set; }
    }

    public class TsumoRes
    {
        public TileId Tsumo { get; set; }
    }

    public class DahaiRes
    {
    }

    public class AgariRes
    {
        public TileIds Tiles { get; set; }
        public TileId WinTile { get; set; }
        public List<Meld> Melds { get; set; }
        public HandResponseModel Result { get; set; }
    }
}