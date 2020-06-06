using System;

namespace mjlib.HandCalculating.YakuList
{
    internal class Tanyao : Yaku
    {
        public override int YakuId => 11;
        public override int TenhouId => 8;
        public override string Name => "Tanyao";
        public override string Japanese => "断么九";
        public override string English => "All Simples";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(HandCalculator hand, object[] args)
        {
            throw new NotImplementedException();
        }
    }
}