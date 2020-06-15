using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Riichi : Yaku
    {
        public override int YakuId => 1;
        public override int TenhouId => 1;
        public override string Name => "Riichi";
        public override string Japanese => "立直";
        public override string English => "Riichi";
        public override int HanOpen { get; set; } = 0;
        public override int HanClosed { get; set; } = 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}