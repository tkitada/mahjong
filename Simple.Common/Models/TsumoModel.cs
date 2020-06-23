using mjlib.Tiles;

namespace Simple.Common.Models
{
    public class TsumoModel
    {
        public TileId TsumoTile { get; set; }
        public int Shanten { get; set; }
        public bool IsAgari { get; set; }
        public bool CanRiichi { get; set; }
    }
}