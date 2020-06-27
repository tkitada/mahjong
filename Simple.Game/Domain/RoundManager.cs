using mjlib.HandCalculating;
using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.HandCalculating.HandCalculator;

namespace Simple.Game.Domain
{
    internal class RoundManager
    {
        private readonly GameOptionalRules rules_;
        private readonly GameInformation gameInfo_;

        private List<int> wall_;
        public TileIds Hand { get; private set; }
        public List<int> Discards { get; private set; }

        private readonly Random random_;

        public RoundManager(GameOptionalRules rules, GameInformation gameInfo)
        {
            rules_ = rules;
            gameInfo_ = gameInfo;

            random_ = new Random();
        }

        internal void Start()
        {
            wall_ = new List<int>(Enumerable.Range(0, 136));
            var count = wall_.Count;
            for (var i = 0; i < count; i++)
            {
                var r = random_.Next(i, count);
                (wall_[i], wall_[r]) = (wall_[r], wall_[i]);
            }

            Hand = new TileIds(wall_.GetRange(0, 13));
            wall_.RemoveRange(0, 13);

            Discards = new List<int>();
        }

        private void Tsumo()
        {
            var tsumoTile = wall_[0];
            Hand.Add(tsumoTile);
            wall_.RemoveAt(0);
        }

        private void Dahai(int index)
        {
            Discards.Add(Hand[index].Value);
            Hand.RemoveAt(index);
        }

        private void Agari(HandConfig config)
        {
            var res = EstimateHandValue(Hand, Hand.Last(), config: config);
            if (!(res.Error is null)) return;
        }
    }
}