using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Honroto : Yaku
    {
        public override int YakuId => 25;

        public override int TenhouId => 31;

        public override string Name => "Honroto";

        public override string Japanese => "混老頭";

        public override string English => "Terminals and Honors";

        public override int HanOpen => 2;

        public override int HanClosed => 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var indices = hand.Aggregate((x, y) => x.AddRange(y));
            return indices.All(x => Constants.YAOCHU_INDICES.Contains(x.Value));
        }
    }
}