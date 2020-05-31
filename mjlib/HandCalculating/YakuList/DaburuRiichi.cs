namespace mjlib.HandCalculating.YakuList
{
    internal class DaburuRiichi : Yaku
    {
        public override int YakuID => 7;
        public override int TenhouID => 21;
        public override string Name => "Double Riichi";
        public override string Japanese => "ダブル立直";
        public override string English => "Double Riichi";
        public override int HanOpen => 0;
        public override int HanClosed => 2;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(Hand hand, object[] args)
        {
            return true;
        }
    }
}