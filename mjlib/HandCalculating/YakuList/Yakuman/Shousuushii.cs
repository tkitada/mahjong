using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Shousuushii : Yaku
    {
        public override int YakuId => 40;

        public override int TenhouId => 50;

        public override string Name => "Shousuushii";

        public override string Japanese => "小四喜";

        public override string English => "Small Four Winds";

        public override int HanOpen => 13;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var ponSets = hand.Where(x => x.IsPon);
            if (ponSets.Count() < 3) return false;

            var countOfWindSets = 0;
            var windPair = 0;
            var winds = new List<int>
            {
                EAST, SOUTH, WEST, NORTH
            };
            foreach (var item in hand)
            {
                if (item.IsPon && winds.Contains(item[0].Value))
                {
                    countOfWindSets++;
                }
                if (item.IsPair && winds.Contains(item[0].Value))
                {
                    windPair++;
                }
            }
            return countOfWindSets == 3 && windPair == 1;
        }
    }
}