using System;
using static mjlib.Constants;


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

        public string ToOneLineString(bool printAkaDora = false)
        {
            if (Value < 36)
            {
                return printAkaDora && Value == FIVE_RED_MAN ? "0m" : $"{Value / 4 + 1}m";
            }
            else if (36 <= Value && Value < 72)
            {
                return printAkaDora && Value == FIVE_RED_PIN ? "0p" : $"{(Value - 36) / 4 + 1}p";
            }
            else if (72 <= Value && Value < 108)
            {
                return printAkaDora && Value == FIVE_RED_SOU ? "0s" : $"{(Value - 72) / 4 + 1}s";
            }
            return $"{(Value - 108) / 4 + 1}z";
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