using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Tenhou : Yaku
    {
        public override int YakuId => 50;

        public override int TenhouId => 37;

        public override string Name => "Tenhou";

        public override string Japanese => "天和";

        public override string English => "Heavenly Hand";

        public override int HanOpen => 13;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}