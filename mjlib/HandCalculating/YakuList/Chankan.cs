namespace mjlib.HandCalculating.YakuList
{
    internal class Chankan : Yaku
    {
        public override int YakuId => 3;
        public override int TenhouId => 3;
        public override string Name => "Chankan";
        public override string Japanese => "搶槓";
        public override string English => "Robbing A Kan";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            return true;
        }
    }
}