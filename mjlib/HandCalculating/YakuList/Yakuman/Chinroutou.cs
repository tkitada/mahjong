using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Chinroutou : Yaku
    {
        public override int YakuId => 44;

        public override int TenhouId => 44;

        public override string Name => "Chinroutou";

        public override string Japanese => "清老頭";

        public override string English => "All Terminals";

        public override int HanOpen => 13;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var indices = hand.Aggregate((x, y) => x.AddRange(y));
            return indices.All(x => Constants.TERMINAL_INDICES.Contains(x.Value));
        }
    }
}