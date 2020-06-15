using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class KokushiMusou : Yaku
    {
        public override int YakuId => 36;

        public override int TenhouId => 47;

        public override string Name => "KokushiMusou";

        public override string Japanese => "国士無双";

        public override string English => "Thirteen Orphans";

        public override int HanOpen { get; set; } = 0;

        public override int HanClosed { get; set; } = 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var tiles34 = (Tiles34)args[0];
            return tiles34[0] * tiles34[8] * tiles34[9] *
                tiles34[17] * tiles34[18] * tiles34[26] *
                tiles34[27] * tiles34[28] * tiles34[29] * tiles34[30] *
                tiles34[31] * tiles34[32] * tiles34[33] == 2;
        }
    }
}