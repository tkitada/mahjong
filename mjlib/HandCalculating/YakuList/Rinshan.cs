﻿namespace mjlib.HandCalculating.YakuList
{
    internal class Rinshan : Yaku
    {
        public override int YakuID => 4;
        public override int TenhouID => 4;
        public override string Name => "Rinshan Kaihou";
        public override string Japanese => "嶺上開花";
        public override string English => "Dead Wall Draw";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(Hand hand, object[] args)
        {
            return true;
        }
    }
}