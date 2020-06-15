using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Honitsu : Yaku
    {
        public override int YakuId => 32;

        public override int TenhouId => 34;

        public override string Name => "Honitsu";

        public override string Japanese => "混一色";

        public override string English => "Half Flush";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var manSets = 0;
            var pinSets = 0;
            var souSets = 0;
            var honorSets = 0;
            foreach (var item in hand)
            {
                if (Constants.HONOR_INDICES.Contains(item[0].Value))
                {
                    honorSets++;
                }
                if (item[0].IsMan)
                {
                    manSets++;
                }
                if (item[0].IsPin)
                {
                    pinSets++;
                }
                if (item[0].IsSou)
                {
                    souSets++;
                }
            }
            var sets = new List<int>
            {
                manSets,
                pinSets,
                souSets
            };
            var onlyOneSuit = sets.Count(x => x != 0) == 1;
            return onlyOneSuit && honorSets != 0;
        }
    }
}