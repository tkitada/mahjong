using mjlib;
using System;

namespace Simple.Game.Domain
{
    internal class GameManager
    {
        internal event EventHandler<GameInfoNotificationEventArgs> GameInfoNotificationEvent;

        internal event EventHandler<HandNotificationEventArgs> HandNotificationEvent;

        internal event EventHandler<TsumoNotificationEventArgs> TsumoNotificationEvent;

        private readonly GameOptionalRules rules_;
        private RoundManager roundManager_;

        private int point_;
        private int playerWind_;
        private int roundWind_;
        private int roundCount_;
        private int honba_;
        private int kyotaku_;

        public GameManager(GameOptionalRules rules)
        {
            rules_ = rules;
        }

        internal void Start()
        {
            point_ = rules_.PrimaryPoint;
            playerWind_ = Constants.EAST;
            roundWind_ = Constants.EAST;
            roundCount_ = 1;
            honba_ = 0;
            kyotaku_ = 0;
        }

        private void Run()
        {
            while (true)
            {
                RoubdInit();
                roundManager_.Start();
                if (IsEnd(false)) break;

            }
        }

        private void RoubdInit()
        {
            var gameInfo = new GameInformation(point_, playerWind_, roundWind_, roundCount_, honba_, kyotaku_);
            roundManager_ = new RoundManager(rules_, gameInfo);

            roundManager_.HandNotificationEvent += (_, e) => HandNotificationEvent(this, e);
            roundManager_.TsumoNotificationEvent += (_, e) => TsumoNotificationEvent(this, e);

            GameInfoNotificationEvent(this, new GameInfoNotificationEventArgs(gameInfo));
        }


        private bool IsEnd(bool renchan)
        {
            if (point_ < 0) return true;
            if (roundWind_ == Constants.SOUTH && roundCount_ == 4 && !renchan && point_ < rules_.ReturnPoint)
                return false;
            if (roundWind_ > Constants.WEST && point_ >= rules_.ReturnPoint)
                return true;
            if (roundWind_ == Constants.NORTH && roundCount_ == 4 && !renchan) return true;
            return false;
        }

        private void UpdateGameInfo()
        {

        }
    }

    internal class GameInformation
    {
        public int Point { get; }
        public int PlayerWind { get; }
        public int RoundWind { get; }
        public int RoundCount { get; set; }
        public int Honba { get; }
        public int Kyoutaku { get; }

        public GameInformation(int point, int playerWind, int roundWind, int roundCount, int honba, int kyoutaku)
        {
            Point = point;
            PlayerWind = playerWind;
            RoundWind = roundWind;
            RoundCount = roundCount;
            Honba = honba;
            Kyoutaku = kyoutaku;
        }
    }
}