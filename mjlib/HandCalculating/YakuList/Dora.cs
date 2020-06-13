using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Dora : Yaku
    {
        public override int YakuId => 53;

        public override int TenhouId => 52;

        public override string Name => "Dora";

        public override string Japanese => "ドラ";

        public override string English => "Dora";

        public override int HanOpen => 1;

        public override int HanClosed => 1;

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