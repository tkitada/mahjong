using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Chinitsu : Yaku
    {
        public override int YakuId => 35;

        public override int TenhouId => 35;

        public override string Name => "Chinitsu";

        public override string Japanese => "清一色";

        public override string English => "Flush";

        public override int HanOpen { get; set; } = 5;

        public override int HanClosed { get; set; } = 6;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var manSets = 0;
            var pinSets = 0;
            var souSets = 0;
            var honorsSets = 0;
            foreach (var item in hand)
            {
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
                if (Constants.HONOR_INDICES.Contains(item[0].Value))
                {
                    honorsSets++;
                }
            }
            var sets = new List<int>
            {
                manSets, pinSets, souSets
            };
            return sets.Count(x => x != 0) == 1
                && honorsSets == 0;
        }
    }
}