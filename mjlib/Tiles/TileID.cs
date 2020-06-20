using System;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌一枚ずつに番号をつけたもの 0～135
    /// </summary>
    public class TileId : IEquatable<TileId>
    {
        public int Value { get; }

        public TileId(int value)
        {
            Value = value;
        }

        public TileKind ToTileKind()
        {
            return new TileKind(Value / 4);
        }

        public static TileId Parse(string man = "", string pin = "", string sou = "",
            string honors = "", bool hasAkaDora = false)
        {
            return TileIds.Parse(man: man, pin: pin, sou: sou, honors: honors, hasAkaDora: hasAkaDora)[0];
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is TileId other
                && Equals(other);
        }

        public bool Equals(TileId other)
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