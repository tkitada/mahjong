using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib
{
    internal class Agari
    {
        public static bool IsAgari(TilesSet tilesSet, List<Tiles34> openSets)
        {
            var tiles = new TilesSet(tilesSet.Select(t => t).ToList());

            if (openSets.Count != 0)
            {
                var isolatedTiles = tiles.FindIsolatedTileIndices();
                foreach (var meld in openSets)
                {
                    if (isolatedTiles.Count == 0) break;

                    var isolatedTile = isolatedTiles[0];
                    isolatedTiles.RemoveAt(0);

                    tiles[meld[0].Value] -= 1;
                    tiles[meld[1].Value] -= 1;
                    tiles[meld[2].Value] -= 1;
                    tiles[isolatedTile.Value] = 3;
                }
            }

        }
    }
}