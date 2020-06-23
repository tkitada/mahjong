using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAloneGame
{
    internal class Program
    {
        private static Random random_;

        private static void Main(string[] args)
        {
            random_ = new Random();
            var wall = new List<int>(Enumerable.Range(0, 136));
            var count = wall.Count;
            for (var i = 0; i < count; i++)
            {
                var r = random_.Next(i, count);
                (wall[i], wall[r]) = (wall[r], wall[i]);
            }
            var hand = new TileIds(wall.GetRange(0, 13));
            var discards = new List<int>();

            wall.RemoveRange(0, 13);

            for (var c = wall.Count; c > 0; c--)
            {
                hand.Add(wall[0]);
                wall.RemoveAt(0);

                PrintTiles(hand);

                var res = HandCalculator.EstimateHandValue(hand, hand.Last()/*, config:new HandConfig(isTsumo:true)*/);
                if (res.Error is null)
                {
                    Console.WriteLine("");
                    PrintHandResult(hand, hand.Last(), null, res);
                    break;
                }
                discards.Add(DecideDahai(hand));

                PrintTiles(hand);
            }
        }

        private static void PrintTiles(TileIds tiles)
        {
            Console.WriteLine(tiles.ToOneLineString());
        }

        private static int DecideDahai(TileIds hand)
        {
            var shantenList = new List<int>();
            for (var i = 0; i < hand.Count; i++)
            {
                var h = new TileIds(hand);
                h.RemoveAt(i);
                shantenList.Add(Shanten.CalculateShanten(h));
            }
            shantenList.ForEach(Console.Write);
            Console.WriteLine();
            var m = shantenList.IndexOf(shantenList.Min());
            var t = hand[m];
            hand.RemoveAt(m);
            return t.Value;
        }

        private static void PrintHandResult(TileIds tiles, TileId winTile, List<Meld> melds, HandResponse result)
        {
            Console.WriteLine($"{tiles.ToOneLineString()}");
            var IsOpened = false;
            if (melds is null)
            {
                Console.WriteLine("鳴きなし");
            }
            else
            {
                foreach (var meldItem in melds)
                {
                    Console.WriteLine(meldItem);
                }
                IsOpened = melds.Count(x => x.Opened) > 0;
            }
            Console.WriteLine($"和了牌: {new TileIds(new List<TileId> { winTile }).ToOneLineString()}");
            foreach (var yakuItem in result.Yaku)
            {
                var han = IsOpened ? yakuItem.HanOpen : yakuItem.HanClosed;
                Console.WriteLine($"{yakuItem.Japanese}\t{han}翻");
            }
            Console.WriteLine($"{result.Han}翻 {result.Fu}符");
            Console.WriteLine($"{result.Cost.Main}点");
            foreach (var fuItem in result.FuDetailSet)
            {
                Console.WriteLine($"符: {fuItem.Fu}\tReason: {fuItem.Reason}");
            }
            Console.WriteLine("");
        }
    }
}