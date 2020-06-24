using mjlib.Tiles;

namespace Simple.Common.Models
{
    public class TsumoNotification
    {
        public TileId TsumoTile { get; }

        public TsumoNotification(TileId tsumoTile)
        {
            TsumoTile = tsumoTile;
        }
    }
}