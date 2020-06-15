using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Chanta : Yaku
    {
        public override int YakuId => 24;

        public override int TenhouId => 23;

        public override string Name => "Chanta";

        public override string Japanese => "混全帯么九";

        public override string English => "Terminal Or Honor In Each Group";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            bool TileInIndices(TileKinds itemSet, List<int> indicesArray)
            {
                foreach (var x in itemSet)
                {
                    if (indicesArray.Contains(x.Value)) return true;
                }
                return false;
            }
            var yaoshuSets = 0;
            var countOfChi = 0;
            foreach (var item in hand)
            {
                if (item.IsChi)
                {
                    countOfChi++;
                }
                if (TileInIndices(item, Constants.YAOCHU_INDICES))
                {
                    yaoshuSets++;
                }
            }
            return countOfChi != 0 && yaoshuSets == 5 && yaoshuSets != 0;
        }
    }
}