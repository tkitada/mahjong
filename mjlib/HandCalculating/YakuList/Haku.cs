using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Haku : Yaku
    {
        public override int YakuId => 13;

        public override int TenhouId => 18;

        public override string Name => "Yakuhai (haku)";

        public override string Japanese => "役牌(白)";

        public override string English => "White Dragon";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return hand.Count(x => x.IsPon && x[0].Value == Constants.HAKU) == 1;
        }
    }
}