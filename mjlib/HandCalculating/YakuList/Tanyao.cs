﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjlib.HandCalculating.YakuList
{
    class Tanyao : Yaku
    {
        public override int YakuID => 11;
        public override int TenhouID => 8;
        public override string Name => "Tanyao";
        public override string Japanese => "断么九";
        public override string English => "All Simples";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;
        public override bool IsConditionMet(Hand hand, object[] args)
        {

        }
    }
}
