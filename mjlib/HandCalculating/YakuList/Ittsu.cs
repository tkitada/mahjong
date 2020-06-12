using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Ittsu : Yaku
    {
        public override int YakuId => 23;

        public override int TenhouId => 24;

        public override string Name => "Ittsu";

        public override string Japanese => "一気通貫";

        public override string English => "Straight";

        public override int HanOpen => 1;

        public override int HanClosed => 2;

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
            var sets = new List<List<TileKinds>>
            {
                manChi, pinChi, souChi
            };
            foreach (var suitItem in sets)
            {
                if (suitItem.Count() < 3) continue;
                var castedSets = new List<TileKinds>();
                foreach (var setItem in suitItem)
                {
                    castedSets.Add(new TileKinds(new List<TileKind>
                    {
                         new TileKind(setItem[0].Simplify),
                         new TileKind(setItem[1].Simplify),
                         new TileKind(setItem[2].Simplify),
                    }));
                }
                return castedSets.Contains(new TileKinds(new List<int> { 0, 1, 2 }))
                    && castedSets.Contains(new TileKinds(new List<int> { 3, 4, 5 }))
                    && castedSets.Contains(new TileKinds(new List<int> { 6, 7, 8 }));
            }
            return false;
        }
    }
}