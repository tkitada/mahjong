using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class DaburuChuurenPoutou : Yaku
    {
        public override int YakuId => 49;

        public override int TenhouId => 46;

        public override string Name => "Daburu Chuuren Poutou";

        public override string Japanese => "純正九蓮宝燈";

        public override string English => "Pure Nine Gates";

        public override int HanOpen { get; set; } = 26;

        public override int HanClosed { get; set; } = 26;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}