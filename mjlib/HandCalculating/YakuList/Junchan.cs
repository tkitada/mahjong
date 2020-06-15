using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Junchan : Yaku
    {
        public override int YakuId => 33;

        public override int TenhouId => 13;

        public override string Name => "Junchan";

        public override string Japanese => "純全帯么九";

        public override string English => "Terminal In Each Meld";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 3;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            bool TileInIndices(TileKinds itemSet, List<int> indicesArray)
            {
                foreach (var x in itemSet)
                {
                    if (indicesArray.Contains(x.Value))
                    {
                        return true;
                    }
                }
                return false;
            }
            var terminalSets = 0;
            var countOfChi = 0;
            foreach (var item in hand)
            {
                if (item.IsChi)
                {
                    countOfChi++;
                }
                if (TileInIndices(item, Constants.TERMINAL_INDICES))
                    terminalSets++;
            }
            return countOfChi != 0 && terminalSets == 5;
        }
    }
}