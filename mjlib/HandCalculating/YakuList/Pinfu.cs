namespace mjlib.HandCalculating.YakuList
{
    internal class Pinfu : Yaku
    {
        public override int YakuID => 10;
        public override int TenhouID => 7;
        public override string Name => "Pinfu";
        public override string Japanese => "平和";
        public override string English => "All Sequences";
        public override int HanOpen => 0;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(Hand hand, object[] args)
        {
            return true;
        }
    }
}