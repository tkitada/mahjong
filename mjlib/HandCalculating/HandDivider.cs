using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating
{
    internal static class HandDivider
    {
        public static Hand DivideHand(Tiles34 tilesSet, IList<Meld> melds = null)
        {
            if (melds is null)
            {
                melds = new List<Meld>();
            }

            var closedHandTilesSet = new Tiles34(tilesSet.Select(x => x));
            var openTileIndices = melds.Count != 0
                ? melds.Select(x => x.TileKinds)
                       .Aggregate((x, y) => new TileKinds(Enumerable.Concat(x, y)))
                : new TileKinds();
            foreach(var openItem in openTileIndices)
            {
                closedHandTilesSet[openItem.Value] -= 1;
            }

        }
    }
}