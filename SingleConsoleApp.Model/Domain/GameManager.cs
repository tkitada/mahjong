using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System;
using System.Collections.Generic;

namespace SingleConsoleApp.Model.Domain
{
    internal class GameManager
    {
        private readonly OptionalRules rules_ = new OptionalRules();
        private readonly Wall wall_;
        private bool isDaburuRiichi_;
        public TileIds DoraIndicators => wall_.DoraIndicators;
        public Hand Hand { get; }
        public List<int> Discards { get; private set; } = new List<int>();
        public bool RiichiMode { get; private set; }
        public bool IsAgari { get; set; }
        public HandResponse Result { get; private set; }
        public bool IsRyukyoku { get; private set; }

        public GameManager()
        {
            wall_ = new Wall();
            Hand = new Hand(wall_.Haipai());
            Tsumo(new HandConfig(isTenhou: true));
        }

        public void Tsumo(HandConfig config)
        {
            var tsumo = wall_.Tsumo();
            if (tsumo is null) return;

            Hand.Tsumo(tsumo);
            var result = HandCalculator.EstimateHandValue(Hand.AllTiles,
                                                          Hand.TsumoTile,
                                                          doraIndicators: DoraIndicators,
                                                          config: config);
            if (result.Error is null)
            {
                IsAgari = true;
                Result = result;
            }
            else
            {
                IsAgari = false;
                Result = null;
            }
        }

        public void Dahai(int index, bool isRiichi)
        {
            if (index != 13)
            {
                index = Hand.Tehai.IndexOf(Hand.SortedTehai[index]);
            }
            if (isRiichi)
            {
                //if (Shanten.CalculateShanten(Hand.AllTiles) != 0) return;
                RiichiMode = true;
                isDaburuRiichi_ = Discards.Count == 0;
            }

            Discards.Add(Hand.Dahai(index).Value);
            if (wall_.RemainCount == 0)
            {
                IsRyukyoku = true;
            }
            else
            {
                Tsumo(new HandConfig(options: rules_,
                                     isTsumo: true,
                                     isRiichi: RiichiMode,
                                     isIppatsu: isRiichi,
                                     isDaburuRiichi: RiichiMode && isDaburuRiichi_,
                                     playerWind: Constants.EAST,
                                     roundWind: Constants.EAST));
            }
        }
    }
}