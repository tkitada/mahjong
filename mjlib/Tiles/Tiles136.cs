using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.Tiles
{
    internal class Tiles136 : IEnumerable<TileID>
    {
        private readonly IList<TileID> tiles_;

        public int Count => tiles_.Count;

        public TileID this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public Tiles136()
        {
            tiles_ = new List<TileID>();
        }

        public Tiles136(IList<TileID> tiles)
        {
            tiles_ = tiles;
        }

        public Tiles136(IList<int> tiles)
        {
            tiles_ = tiles.Select(t => new TileID(t)).ToList();
        }

        public void Add(TileID item)
        {
            tiles_.Add(item);
        }

        public bool Contains(TileID item)
        {
            return tiles_.Contains(item);
        }

        public IEnumerator<TileID> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }
    }
}