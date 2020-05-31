﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjlib.HandCalculating.YakuList
{
    class Renhou : Yaku
    {
        public override int YakuID => 9;
        public override int TenhouID => 36;
        public override string Name => "Renhou";
        public override string Japanese => "人和";
        public override string English => "Hand Of Man";
        public override int HanOpen => 0;
        public override int HanClosed => 5;
        public override bool IsYakuman => false;
        public override bool IsConditionMet(Hand hand, object[] args)
        {
            return true;
        }
    }
}