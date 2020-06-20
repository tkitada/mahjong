using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Dora : Yaku
    {
        public override int YakuId => 53;

        public override int TenhouId => 52;

        public override string Name => $"Dora{HanClosed}";

        public override string Japanese => $"ドラ{HanClosed}";

        public override string English => $"Dora{HanClosed}";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Dora {HanClosed}";
        }
    }
}