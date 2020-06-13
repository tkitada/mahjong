using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Chiihou : Yaku
    {
        public override int YakuId => 51;

        public override int TenhouId => 38;

        public override string Name => "Chiihou";

        public override string Japanese => "地和";

        public override string English => "Earthly Hand";

        public override int HanOpen => 13;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}