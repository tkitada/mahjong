using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Tsuuiisou : Yaku
    {
        public override int YakuId => 43;

        public override int TenhouId => 42;

        public override string Name => "Tsuuiisou";

        public override string Japanese => "字一色";

        public override string English => "All Honors";

        public override int HanOpen { get; set; } = 13;

        public override int HanClosed { get; set; } = 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var indices = hand.Aggregate((x, y) => x.AddRange(y));
            return indices.All(x => Constants.HONOR_INDICES.Contains(x.Value));
        }
    }
}