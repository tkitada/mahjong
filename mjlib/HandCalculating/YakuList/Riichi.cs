namespace mjlib.HandCalculating.YakuList
{
    internal class Riichi : Yaku
    {
        public override int YakuId => 1;
        public override int TenhouId => 1;
        public override string Name => "Riichi";
        public override string Japanese => "立直";
        public override string English => "Riichi";
        public override int HanOpen => 0;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            return true;
        }
    }
}