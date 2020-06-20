using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Sanshoku : Yaku
    {
        public override int YakuId => 22;

        public override int TenhouId => 25;

        public override string Name => "Sanshoku Doujun";

        public override string Japanese => "三色同順";

        public override string English => "Three Colored Triplets";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var chiSets = hand.Where(x => x.IsChi);
            if (chiSets.Count() < 3) return false;

            var manChi = new List<TileKinds>();
            var pinChi = new List<TileKinds>();
            var souChi = new List<TileKinds>();
            foreach (var item in chiSets)
            {
                if (item[0].IsMan)
                {
                    manChi.Add(item);
                }
                if (item[0].IsPin)
                {
                    pinChi.Add(item);
                }
                if (item[0].IsSou)
                {
                    souChi.Add(item);
                }
            }
            foreach (var manItem in manChi)
            {
                foreach (var pinItem in pinChi)
                {
                    foreach (var souItem in souChi)
                    {
                        var manNum = new TileKinds(manItem.Select(x => x.Simplify));
                        var pinNum = new TileKinds(pinItem.Select(x => x.Simplify));
                        var souNum = new TileKinds(souItem.Select(x => x.Simplify));
                        if (manNum.Equals(pinNum) && pinNum.Equals(souNum)) return true;
                    }
                }
            }
            return false;
        }
    }
}