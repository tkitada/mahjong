namespace mjlib.HandCalculating
{
    internal abstract class Yaku
    {
        public abstract int YakuID { get; }
        public abstract int TenhouID { get; }
        public abstract string Name { get; }
        public abstract string Japanese { get; }
        public abstract string English { get; }
        public abstract int HanOpen { get; }
        public abstract int HanClosed { get; }
        public abstract bool IsYakuman { get; }

        public abstract bool IsConditionMet(Hand hand, object[] args);
    }
}