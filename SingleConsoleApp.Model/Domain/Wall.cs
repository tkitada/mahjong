using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleConsoleApp.Model.Domain
{
    internal class Wall
    {
        public event EventHandler<EventArgs> RyukyokuEvent;

        private readonly Random random_ = new Random();
        private readonly List<int> tiles_ = new List<int>(Enumerable.Range(0, 136));
        private List<int> usefulTiles_;

        public int Count => tiles_.Count;
        public int RemainCount => Count - 14;
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
            usefulTiles_ = tiles_.GetRange(0, 18);
            return haipai;
        }

        public TileId Tsumo()
        {
            if (usefulTiles_.Count == 0)
            {
                RyukyokuEvent?.Invoke(this, EventArgs.Empty);
                return null;
            }
            var tsumo = new TileId(usefulTiles_[0]);
            usefulTiles_.RemoveAt(0);
            return tsumo;
        }
    }
}