namespace mjlib.HandCalculating.YakuList
{
    internal class NagashiMangan : Yaku
    {
        public override int YakuId => 8;
        public override int TenhouId => -1;
        public override string Name => "Nagashi Mangan";
        public override string Japanese => "流し満貫";
        public override string English => "Nagashi Mangan";
        public override int HanOpen => 5;
        public override int HanClosed => 5;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            return true;
        }
    }
}