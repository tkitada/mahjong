using mjlib.Tiles;
using System.Linq;

namespace SingleConsoleApp.Model.Domain
{
    public class Hand
    {
        public TileIds AllTiles => new TileIds(Tehai) { TsumoTile };
        public TileIds Tehai { get; private set; }
        public TileIds SortedTehai => new TileIds(Tehai.OrderBy(x => x.Value));
        public TileId TsumoTile { get; private set; }

        public Hand(TileIds tehai)
        {
            Tehai = tehai;
        }

        public void Tsumo(TileId tsumoTile)
        {
            TsumoTile = tsumoTile;
        }

        public TileId Dahai(int index)
        {
            TileId dahai;
            if (index == 13)
            {
                dahai = TsumoTile;
                TsumoTile = null;
            }
            else
            {
                dahai = Tehai[index];
                Tehai.RemoveAt(index);
                Tehai.Add(TsumoTile);
            }
            return dahai;
        }

        public void Dahai(TileId dahai)
        {
            Tehai.Remove(dahai);
            Tehai.Add(TsumoTile);
        }
    }
}