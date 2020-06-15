using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class DaburuRiichi : Yaku
    {
        public override int YakuId => 7;
        public override int TenhouId => 21;
        public override string Name => "Double Riichi";
        public override string Japanese => "ダブル立直";
        public override string English => "Double Riichi";
        public override int HanOpen { get; set; } = 0;
        public override int HanClosed { get; set; } = 2;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}