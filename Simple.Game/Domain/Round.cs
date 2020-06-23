using mjlib.HandCalculating;
using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.HandCalculating.HandCalculator;

namespace Simple.Game.Domain
{
    internal class Round
    {
        public List<int> Wall { get; private set; }
        public TileIds Hand { get; private set; }
        public List<int> Discards { get; private set; }

        private readonly Random random_;

        public Round()
        {
            Wall = new List<int>(Enumerable.Range(0, 136));
            var count = Wall.Count;
            for (var i = 0; i < count; i++)
            {
                var r = random_.Next(i, count);
                (Wall[i], Wall[r]) = (Wall[r], Wall[i]);
            }
            Discards = new List<int>();
        }

        private void Haipai()
        {
            Hand = new TileIds(Wall.GetRange(0, 13));
            Wall.RemoveRange(0, 13);
        }

        private void Tsumo()
        {
            Hand.Add(Wall[0]);
            Wall.RemoveAt(0);
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