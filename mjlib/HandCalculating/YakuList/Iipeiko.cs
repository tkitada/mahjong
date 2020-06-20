using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Iipeiko : Yaku
    {
        public override int YakuId => 12;

        public override int TenhouId => 9;

        public override string Name => "Iipeiko";

        public override string Japanese => "一盃口";

        public override string English => "Identical Sequences";

        public override int HanOpen { get; set; } = 0;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var chiSets = hand.Where(x => x.IsChi);

            var countOfIdenticalChi = 0;
            foreach (var x in chiSets)
            {
                var count = 0;
                foreach (var y in chiSets)
                {
                    if (x.Equals(y))
                    {
                        count++;
                    }
                }
                if (count > countOfIdenticalChi)
                {
                    countOfIdenticalChi = count;
                }
            }
            return countOfIdenticalChi >= 2;
        }
    }
}