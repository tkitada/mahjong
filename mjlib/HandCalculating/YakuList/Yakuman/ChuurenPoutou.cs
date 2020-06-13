using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class ChuurenPoutou : Yaku
    {
        public override int YakuId => 37;

        public override int TenhouId => 45;

        public override string Name => "ChuurenPoutou";

        public override string Japanese => "九蓮宝燈";

        public override string English => "Nine Gates";

        public override int HanOpen => 0;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var manSets = 0;
            var pinSets = 0;
            var souSets = 0;
            var honorsSets = 0;
            foreach (var item in hand)
            {
                if (item[0].IsMan)
                    manSets++;
                if (item[0].IsPin)
                    pinSets++;
                if (item[0].IsSou)
                    souSets++;
                if (Constants.HONOR_INDICES.Contains(item[0].Value))
                    honorsSets++;
            }
            var sets = new List<int>
            {
                manSets, pinSets, souSets
            };
            if (sets.Count(x => x != 0) != 1 || honorsSets > 0) return false;

            var __indices = hand.Aggregate((x, y) => x.AddRange(y));
            var _indices = __indices.Select(x => x.Simplify);
            if (_indices.Count(x => x == 0) < 3) return false;
            if (_indices.Count(x => x == 8) < 3) return false;
            var indices = _indices.ToList();
            indices.Remove(0);
            indices.Remove(0);
            indices.Remove(8);
            indices.Remove(8);
            for (var x = 0; x < 9; x++)
                if (indices.Contains(x))
                    indices.Remove(x);
            return indices.Count == 1;
        }
    }
}