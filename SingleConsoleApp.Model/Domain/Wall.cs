using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleConsoleApp.Model.Domain
{
    internal class Wall
    {
        private readonly Random random_ = new Random();
        private readonly List<int> tiles_ = new List<int>(Enumerable.Range(0, 136));

        public int Count => tiles_.Count;
        public int RemainCount => Count - 105;
        public TileIds DoraIndicators { get; }

        public Wall()
        {
            var count = tiles_.Count;
            for (var i = 0; i < count; i++)
            {
                var r = random_.Next(i, count);
                (tiles_[i], tiles_[r]) = (tiles_[r], tiles_[i]);
            }
            DoraIndicators = new TileIds { tiles_[Count - 5] };
        }

        public TileIds Haipai()
        {
            var haipai = new TileIds(tiles_.GetRange(0, 13));
            tiles_.RemoveRange(0, 13);
            return haipai;
        }

        public TileId Tsumo()
        {
            var tsumo = new TileId(tiles_[0]);
            tiles_.RemoveAt(0);
            return tsumo;
        }
    }
}