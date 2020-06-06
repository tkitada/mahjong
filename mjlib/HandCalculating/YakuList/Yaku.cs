namespace mjlib.HandCalculating
{
    internal abstract class Yaku
    {
        public abstract int YakuId { get; }
        public abstract int TenhouId { get; }
        public abstract string Name { get; }
        public abstract string Japanese { get; }
        public abstract string English { get; }
        public abstract int HanOpen { get; }
        public abstract int HanClosed { get; }
        public abstract bool IsYakuman { get; }

        public abstract bool IsConditionMet(HandCalculator hand, object[] args);
    }
}