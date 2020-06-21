using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    public class TileKinds : IEnumerable<TileKind>, IEquatable<TileKinds>, IComparable<TileKinds>
    {
        private readonly List<TileKind> tiles_;

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
            Count == 2;

        public TileKind this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public TileKinds()
        {
            tiles_ = new List<TileKind>();
        }

        public TileKinds(IEnumerable<TileKind> tiles)
        {
            tiles_ = tiles.ToList();
        }

        public TileKinds(IEnumerable<int> tiles)
        {
            tiles_ = tiles.Select(t => new TileKind(t)).ToList();
        }

        public void Add(TileKind item)
        {
            tiles_.Add(item);
        }

        public TileKinds AddRange(TileKinds collection)
        {
            var t = tiles_.ToList();
            t.AddRange(collection);
            return new TileKinds(t);
        }

        public bool Contains(TileKind item)
        {
            return tiles_.Contains(item);
        }

        public int IndexOf(TileKind item)
        {
            return tiles_.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            tiles_.RemoveAt(index);
        }

        public IEnumerator<TileKind> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }

        public bool ContainsTerminal()
        {
            return this.Any(x => TERMINAL_INDICES.Contains(x.Value));
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is TileKinds other
                && Equals(other);
        }

        public bool Equals(TileKinds other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;
            for (var i = 0; i < tiles_.Count; i++)
            {
                if (!this[i].Equals(other[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(TileKinds other)
        {
            var min = Math.Min(Count, other.Count);
            for (var i = 0; i < min; i++)
            {
                if (this[i].CompareTo(other[i]) > 0) return 1;
                if (this[i].CompareTo(other[i]) < 0) return -1;
            }
            return Count > other.Count ? 1 : Count < other.Count ? -1 : 0;
        }
    }
}