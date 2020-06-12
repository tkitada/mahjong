using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Chiitoitsu : Yaku
    {
        public override int YakuId => 30;

        public override int TenhouId => 22;

        public override string Name => "Chiitoitsu";

        public override string Japanese => "七対子";

        public override string English => "Seven Pairs";

        public override int HanOpen => 0;

        public override int HanClosed => 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return hand.Count() == 7;
        }
    }
}