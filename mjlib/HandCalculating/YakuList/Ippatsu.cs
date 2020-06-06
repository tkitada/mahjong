namespace mjlib.HandCalculating.YakuList
{
    internal class Ippatsu : Yaku
    {
        public override int YakuID => 2;
        public override int TenhouID => 2;
        public override string Name => "Ippatsu";
        public override string Japanese => "一発";
        public override string English => "One Shot";
        public override int HanOpen => 0;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            return true;
        }
    }
}