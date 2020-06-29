using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using Simple.Common.Models;
using System;
using System.Collections.Generic;

namespace Simple.Game.Domain
{
    internal class GameManager
    {
        private readonly GameOptionalRules rules_;
        private readonly GameInformation gameInfo_;
        private RoundManager roundManager_;

        internal GameManager(GameOptionalRules rules)
        {
            rules_ = rules;
            gameInfo_ = new GameInformation
            {
                Point = rules_.PrimaryPoint,
                PlayerWind = Constants.EAST,
                RoundWind = Constants.EAST,
                RoundCount = 1,
                Honba = 0,
                Kyoutaku = 0
            };
        }

        public void Start()
        {
            roundManager_ = new RoundManager(rules_, gameInfo_);
            roundManager_.Start();
        }

        public TileIds Hand => roundManager_.Hand;

        public TileId Tsumo()
        {
            return roundManager_.Tsumo();
        }

        public void Dahai(int index)
        {
            roundManager_.Dahai(index);
        }

        public (TileIds, TileId, List<Meld>, HandResponseModel) Agari()
        {
            return roundManager_.Agari();
        }
    }

    internal class GameInformation
    {
        public int Point { get; set; }
        public int PlayerWind { get; set; }
        public int RoundWind { get; set; }
        public int RoundCount { get; set; }
        public int Honba { get; set; }
        public int Kyoutaku { get; set; }
    }
}