using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class SanshokuDoukou : Yaku
    {
        public override int YakuId => 29;

        public override int TenhouId => 26;

        public override string Name => "SanshokuDoukou";

        public override string Japanese => "三色同刻";

        public override string English => "Three Colored Triplets";

        public override int HanOpen => 2;

        public override int HanClosed => 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var ponSets = hand.Where(x => x.IsPon);
            if (ponSets.Count() < 3) return false;

            var manPon = new List<TileKinds>();
            var pinPon = new List<TileKinds>();
            var souPon = new List<TileKinds>();
            foreach (var item in ponSets)
            {
                if (item[0].IsMan)
                {
                    manPon.Add(item);
                }
                if (item[0].IsPin)
                {
                    pinPon.Add(item);
                }
                if (item[0].IsSou)
                {
                    souPon.Add(item);
                }
            }
            foreach (var manItem in manPon)
            {
                foreach (var pinItem in pinPon)
                {
                    foreach (var souItem in souPon)
                    {
                        var manSimple = manItem.Select(x => x.Simplify);
                        var pinSimple = pinItem.Select(x => x.Simplify);
                        var souSimple = souItem.Select(x => x.Simplify);
                        if (manSimple.SequenceEqual(pinSimple) && pinSimple.SequenceEqual(souSimple))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}