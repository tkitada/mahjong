﻿using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class YakuhaiEast : Yaku
    {
        public override int YakuId => 16;

        public override int TenhouId => 10;

        public override string Name => "Yakuhai (east)";

        public override string Japanese => "役牌(東)";

        public override string English => "East Round/Seat";

        public override int HanOpen { get; set; } = 1;

        public override int HanClosed { get; set; } = 1;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var playerWind = (int)args[0];
            var round_wind = (int)args[1];

            return hand.Count(
                x => x.IsPon && x[0].Value == playerWind) == 1
                    && playerWind == Constants.EAST
                || hand.Count(
                x => x.IsPon && x[0].Value == round_wind) == 1
                    && round_wind == Constants.EAST;
        }
    }
}