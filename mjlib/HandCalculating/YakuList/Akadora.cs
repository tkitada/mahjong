using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Akadora : Yaku
    {
        public override int YakuId => 54;

        public override int TenhouId => 54;

        public override string Name => "Aka Dora";

        public override string Japanese => "赤ドラ";

        public override string English => "Red Five";

        public override int HanOpen => 1;

        public override int HanClosed => 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Aka Dora {HanClosed}";
        }
    }
}