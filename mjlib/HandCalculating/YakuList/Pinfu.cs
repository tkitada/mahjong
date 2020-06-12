using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Pinfu : Yaku
    {
        public override int YakuId => 10;
        public override int TenhouId => 7;
        public override string Name => "Pinfu";
        public override string Japanese => "平和";
        public override string English => "All Sequences";
        public override int HanOpen => 0;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}