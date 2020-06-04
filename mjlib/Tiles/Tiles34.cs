using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.Tiles
{
    public class Tiles34 : IEnumerable<TileKind>
    {
        private readonly IList<TileKind> tiles_;

        public int Count => tiles_.Count;

        public bool IsChi =>
            Count == 3
            && this[0].Value == this[1].Value - 1
            && this[1].Value == this[2].Value - 1;

        public bool IsPon =>
            Count == 3
            && this[0].Value == this[1].Value
            && this[1].Value == this[2].Value;

        public bool IsPair =>
            Count == 2
            && this[0].Value == this[1].Value;

        public TileKind this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public Tiles34()
        {
            tiles_ = new List<TileKind>();
        }

        public Tiles34(IList<TileKind> tiles)
        {
            tiles_ = tiles;
        }

        public Tiles34(IList<int> tiles)
        {
            tiles_ = tiles.Select(t => new TileKind(t)).ToList();
        }

        public void Add(TileKind item)
        {
            tiles_.Add(item);
        }

        public bool Contains(TileKind item)
        {
            return tiles_.Contains(item);
        }

        public IEnumerator<TileKind> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }
    }
}