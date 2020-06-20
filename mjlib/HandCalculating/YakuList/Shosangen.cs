using mjlib.Tiles;
using System.Collections.Generic;
using static mjlib.Constants;

namespace mjlib.HandCalculating.YakuList
{
    internal class Shosangen : Yaku
    {
        public override int YakuId => 31;

        public override int TenhouId => 30;

        public override string Name => "Shosangen";

        public override string Japanese => "小三元";

        public override string English => "Small Three Dragons";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var dragons = new List<int>
            {
                HAKU, HATSU, CHUN
            };
            var countOfConditions = 0;
            foreach (var item in hand)
            {
                if ((item.IsPair || item.IsPon) && dragons.Contains(item[0].Value))
                {
                    countOfConditions++;
                }
            }
            return countOfConditions == 3;
        }
    }
}