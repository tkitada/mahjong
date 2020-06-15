using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Ryanpeikou : Yaku
    {
        public override int YakuId => 34;

        public override int TenhouId => 32;

        public override string Name => "Ryanpeikou";

        public override string Japanese => "二盃口";

        public override string English => "Two Sets Of Identical Sequences";

        public override int HanOpen { get; set; } = 0;

        public override int HanClosed { get; set; } = 3;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var chiSets = hand.Where(i => i.IsChi);
            var countOfIdenticalChi = new List<int>();
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
                countOfIdenticalChi.Append(count);
            }
            return countOfIdenticalChi.Count(x => x >= 2) == 4;
        }
    }
}