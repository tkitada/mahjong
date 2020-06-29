using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using Simple.Common.Models;
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

        public void Start()
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

        public TileId Tsumo()
        {
            var tsumoTile = wall_[0];
            Hand.Add(tsumoTile);
            wall_.RemoveAt(0);
            return new TileId(tsumoTile);
        }

        public void Dahai(int index)
        {
            Discards.Add(Hand[index].Value);
            Hand.RemoveAt(index);
        }

        public (TileIds, TileId, List<Meld>, HandResponseModel) Agari()
        {
            var config = new HandConfig(isTsumo: true);
            var tiles = Hand;
            var winTile = Hand.Last();
            List<Meld> melds = null;
            var result = EstimateHandValue(Hand, Hand.Last(), config: config);
            var _res = new HandResponseModel
            {
                Cost = new CostModel
                {
                    Main = result.Cost.Main,
                    Additional = result.Cost.Additional
                },
                Han = result.Han,
                Fu = result.Fu,
                Yaku = result.Yaku.Select(x => new YakuModel
                {
                    YakuId = x.YakuId,
                    TenhouId = x.TenhouId,
                    Name = x.Name,
                    Japanese = x.Japanese,
                    English = x.English,
                    HanOpen = x.HanOpen,
                    HanClosed = x.HanClosed,
                    IsYakuman = x.IsYakuman
                }).ToList(),
                Error = result.Error,
                FuDetailSet = result.FuDetailSet.Select(x => new FuDetailModel
                {
                    Fu = x.Fu,
                    Reason = x.Reason
                }).ToList()
            };
            return (tiles, winTile, melds, _res);
        }
    }
}