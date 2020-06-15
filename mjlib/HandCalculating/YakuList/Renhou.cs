using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Renhou : Yaku
    {
        public override int YakuId => 9;
        public override int TenhouId => 36;
        public override string Name => "Renhou";
        public override string Japanese => "人和";
        public override string English => "Hand Of Man";
        public override int HanOpen { get; set; } = 0;
        public override int HanClosed { get; set; } = 5;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}