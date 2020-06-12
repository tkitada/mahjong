using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class YakuhaiWest : Yaku
    {
        public override int YakuId => 18;

        public override int TenhouId => 10;

        public override string Name => "Yakuhai (west)";

        public override string Japanese => "役牌(西)";

        public override string English => "West Round/Seat";

        public override int HanOpen => 1;

        public override int HanClosed => 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var playerWind = (int)args[0];
            var round_wind = (int)args[1];

            return hand.Where(
                x => x.IsPon && x[0].Value == playerWind).Count() == 1
                    && playerWind == Constants.WEST
                || hand.Where(
                x => x.IsPon && x[0].Value == round_wind).Count() == 1
                    && round_wind == Constants.WEST;
        }
    }
}