using System;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌の種類ごとに番号を付けたもの 0～33
    /// </summary>
    public class TileKind : IEquatable<TileKind>
    {
        public int Value { get; }

        public bool IsMan => Value <= 8;
        public bool IsPin => 8 < Value && Value <= 17;
        public bool IsSou => 17 < Value && Value <= 26;
        public bool IsHonor => Value >= 27;
        public bool IsTerminal => TERMINAL_INDICES.Contains(Value);

        public int Simplify => Value - 9 * (Value / 9);

        public TileKind(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is TileKind other
                && Equals(other);
        }

        public bool Equals(TileKind other)
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