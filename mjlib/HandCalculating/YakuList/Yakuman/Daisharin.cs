using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Daisharin : Yaku
    {
        public override int YakuId => 45;

        public override int TenhouId => -1;

        public override string Name => "Daisharin";

        public override string Japanese => "大車輪";

        public override string English => "Big wheels";

        public override int HanOpen { get; set; } = 0;

        public override int HanClosed { get; set; } = 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var manSets = 0;
            var pinSets = 0;
            var souSets = 0;
            var honorSets = 0;
            foreach (var item in hand)
            {
                if (item[0].IsMan)
                {
                    manSets++;
                }
                else if (item[0].IsPin)
                {
                    pinSets++;
                }
                else if (item[0].IsSou)
                {
                    souSets++;
                }
                else
                {
                    honorSets++;
                }
            }
            var sets = new List<int>
            {
                manSets, pinSets, souSets
            };
            if (sets.Count(x => x != 0) != 1 || pinSets == 0) return false;

            var _indices = hand.Aggregate((x, y) => x.AddRange(y));
            var indices = _indices.Select(x => x.Simplify);
            for (var x = 1; x < 8; x++)
            {
                if (indices.Count(y => y == x) != 2) return false;
            }
            return true;
        }
    }
}