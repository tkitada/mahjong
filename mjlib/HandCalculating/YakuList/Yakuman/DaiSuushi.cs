using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class DaiSuushi : Yaku
    {
        public override int YakuId => 46;

        public override int TenhouId => 49;

        public override string Name => "Dai Suushi";

        public override string Japanese => "大四喜";

        public override string English => "Big Four Winds";

        public override int HanOpen => 26;

        public override int HanClosed => 26;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var ponSets = hand.Where(x => x.IsPon);
            if (ponSets.Count() != 4) return false;
            var countWindSets = 0;
            var winds = new List<int>
            {
                EAST, SOUTH, WEST, NORTH
            };
            foreach (var item in ponSets)
            {
                if (item.IsPon && winds.Contains(item[0].Value))
                {
                    countWindSets++;
                }
            }
            return countWindSets == 4;
        }
    }
}