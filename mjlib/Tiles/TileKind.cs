using static mjlib.Constants;

namespace mjlib.Tiles
{
    public class TileKind
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
    }
}