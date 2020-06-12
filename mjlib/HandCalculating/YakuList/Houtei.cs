using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Houtei : Yaku
    {
        public override int YakuId => 6;
        public override int TenhouId => 6;
        public override string Name => "Houtei Raoyui";
        public override string Japanese => "河底撈魚";
        public override string English => "Win by last discard";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}