using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Hatsu : Yaku
    {
        public override int YakuId => 14;

        public override int TenhouId => 19;

        public override string Name => "Yakuhai (hatsu)";

        public override string Japanese => "役牌(發)";

        public override string English => "Green Dragon";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return hand.Count(x => x.IsPon && x[0].Value == Constants.HATSU) == 1;
        }
    }
}