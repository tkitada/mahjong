namespace mjlib.HandCalculating.YakuList
{
    internal class Haitei : Yaku
    {
        public override int YakuID => 5;
        public override int TenhouID => 5;
        public override string Name => "Haitei Raoyue";
        public override string Japanese => "海底摸月";
        public override string English => "Win By Last Draw";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            return true;
        }
    }
}