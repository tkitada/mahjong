using mjlib;
using mjlib.Tiles;
using System;

namespace Simple.Game.Domain
{
    internal class GameManager
    {
        internal event EventHandler<GameInfoNotificationEventArgs> GameInfoNotificationEvent;

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

        private void UpdateGameInfo()
        {
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