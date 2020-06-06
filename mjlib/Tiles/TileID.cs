using System;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌一枚ずつに番号をつけたもの 0～135
    /// </summary>
    public class TileID : IEquatable<TileID>
    {
        public int Value { get; }

        public TileID(int value)
        {
            Value = value;
        }

        public TileKind ToTileKind()
        {
            return new TileKind(Value / 4);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is TileID other
                && Equals(other);
        }

        public bool Equals(TileID other)
        {
            return other != null
                && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}