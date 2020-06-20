using mjlib.HandCalculating.YakuList;
using mjlib.HandCalculating.YakuList.Yakuman;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Agari;
using static mjlib.Constants;
using static mjlib.HandCalculating.FuCalculator;
using static mjlib.HandCalculating.HandDivider;
using static mjlib.HandCalculating.ScoresCalcurator;

namespace mjlib.HandCalculating
{
    public static class HandCalculator
    {
        private static HandConfig config_;

        public static HandResponse EstimateHandValue(TileIds tiles,
            TileId winTile,
            List<Meld> melds = null,
            TileIds doraIndicators = null,
            HandConfig config = null)
        {
            if (melds is null)
            {
                melds = new List<Meld>();
            }
            if (doraIndicators is null)
            {
                doraIndicators = new TileIds();
            }
            config_ = config ?? new HandConfig();

            var handYaku = new List<Yaku>();
            var tiles34 = tiles.ToTiles34();
            var openedMelds = melds.Where(x => x.Opend)
                                 .Select(x => x.TileKinds)
                                 .ToList();
            var allMelds = melds.Select(x => x.TileKinds).ToList();
            var isOpenHand = openedMelds.Count() > 0;

            if (config_.IsNagashiMangan)
            {
                handYaku.Add(new NagashiMangan());
                var fu = 30;
                var han = new NagashiMangan().HanClosed;
                var cost = CalculateScores(han, fu, config_, false);
                return new HandResponse(cost, han, fu, handYaku);
            }

            if (!tiles.Contains(winTile))
            {
                return new HandResponse(error: "Win tile not in the hand");
            }

            if (config_.IsRiichi && isOpenHand)
            {
                return new HandResponse(error: "Riichi can't be declared with open hand");
            }
            if (config_.IsDaburuRiichi && isOpenHand)
            {
                return new HandResponse(error: "Daburu Riichi can't be declared with open hand");
            }
            if (config_.IsIppatsu && isOpenHand)
            {
                return new HandResponse(error: "Ippatsu can't be declared with open hand");
            }

            if (config_.IsIppatsu && !config_.IsRiichi && !config_.IsDaburuRiichi)
            {
                return new HandResponse(error: "Ippatsu can't be declared without riichi");
            }

            if (!IsAgari(tiles34, allMelds))
            {
                return new HandResponse(error: "Hand is not winning");
            }

            var handOptions = DivideHand(tiles34, melds);
            var calculatedHands = new List<HandResponse>();
            foreach (var hand in handOptions)
            {
                var isChiitoitsu = new Chiitoitsu().IsConditionMet(hand);
                var valuedTiles = new List<int>
                {
                    HAKU, HATSU, CHUN,
                    config_.PlayerWind, config_.RoundWind,
                };
                var winGroups = FindWinGroups(winTile, hand, openedMelds);
                foreach (var winGroup in winGroups)
                {
                    Cost cost = null;
                    string error = null;
                    handYaku = new List<Yaku>();
                    var han = 0;
                    var (fuDetails, fu) = CalculateFu(
                        hand, winTile, winGroup, config_, valuedTiles, melds);
                    var isPinfu = fuDetails.Count == 1 && !isChiitoitsu && !isOpenHand;
                    var ponSets = hand.Where(x => x.IsPon);
                    var chiSets = hand.Where(x => x.IsChi);
                    if (config_.IsTsumo)
                    {
                        if (!isOpenHand)
                        {
                            handYaku.Add(new Tsumo());
                        }
                    }
                    if (isPinfu)
                    {
                        handYaku.Add(new Pinfu());
                    }
                    if (isChiitoitsu && isOpenHand) continue;
                    if (isChiitoitsu)
                    {
                        handYaku.Add(new Chiitoitsu());
                    }
                    var isDaisharin = new Daisharin().IsConditionMet(hand);
                    if (config_.Options.HasDaisharin && isDaisharin)
                    {
                        handYaku.Add(new Daisharin());
                    }
                    var isTanyao = new Tanyao().IsConditionMet(hand);
                    if (isOpenHand && !config_.Options.HasOpenTanyao)
                    {
                        isTanyao = false;
                    }
                    if (isTanyao)
                    {
                        handYaku.Add(new Tanyao());
                    }
                    if (config_.IsRiichi && !config_.IsDaburuRiichi)
                    {
                        handYaku.Add(new Riichi());
                    }
                    if (config_.IsDaburuRiichi)
                    {
                        handYaku.Add(new DaburuRiichi());
                    }
                    if (config_.IsIppatsu)
                    {
                        handYaku.Add(new Ippatsu());
                    }
                    if (config_.IsRinshan)
                    {
                        handYaku.Add(new Rinshan());
                    }
                    if (config_.IsChankan)
                    {
                        handYaku.Add(new Chankan());
                    }
                    if (config_.IsHaitei)
                    {
                        handYaku.Add(new Haitei());
                    }
                    if (config_.IsHoutei)
                    {
                        handYaku.Add(new Houtei());
                    }
                    if (config_.IsRenhou)
                    {
                        if (config_.Options.RenhouAsYakuman)
                        {
                            handYaku.Add(new RenhouYakuman());
                        }
                        else
                        {
                            handYaku.Add(new Renhou());
                        }
                    }
                    if (config_.IsTenhou)
                    {
                        handYaku.Add(new Tenhou());
                    }
                    if (config_.IsChiihou)
                    {
                        handYaku.Add(new Chiihou());
                    }
                    if (new Honitsu().IsConditionMet(hand))
                    {
                        handYaku.Add(new Honitsu());
                    }
                    if (new Chinitsu().IsConditionMet(hand))
                    {
                        handYaku.Add(new Chinitsu());
                    }
                    if (new Tsuuiisou().IsConditionMet(hand))
                    {
                        handYaku.Add(new Tsuuiisou());
                    }
                    if (new Honroto().IsConditionMet(hand))
                    {
                        handYaku.Add(new Honroto());
                    }
                    if (new Chinroutou().IsConditionMet(hand))
                    {
                        handYaku.Add(new Chinroutou());
                    }
                    if (new Ryuuiisou().IsConditionMet(hand))
                    {
                        handYaku.Add(new Ryuuiisou());
                    }

                    //順子が必要な役
                    if (chiSets.Count() != 0)
                    {
                        if (new Chanta().IsConditionMet(hand))
                        {
                            handYaku.Add(new Chanta());
                        }
                        if (new Junchan().IsConditionMet(hand))
                        {
                            handYaku.Add(new Junchan());
                        }
                        if (new Ittsu().IsConditionMet(hand))
                        {
                            handYaku.Add(new Ittsu());
                        }
                        if (!isOpenHand)
                        {
                            if (new Ryanpeikou().IsConditionMet(hand))
                            {
                                handYaku.Add(new Ryanpeikou());
                            }
                            else if (new Iipeiko().IsConditionMet(hand))
                            {
                                handYaku.Add(new Iipeiko());
                            }
                        }
                        if (new Sanshoku().IsConditionMet(hand))
                        {
                            handYaku.Add(new Sanshoku());
                        }
                    }

                    //刻子が必要な役
                    if (ponSets.Count() != 0)
                    {
                        if (new Toitoi().IsConditionMet(hand))
                        {
                            handYaku.Add(new Toitoi());
                        }
                        if (new Sanankou().IsConditionMet(hand, new object[]
                        {
                            winTile, melds, config_.IsTsumo
                        }))
                        {
                            handYaku.Add(new Toitoi());
                        }
                        if (new SanshokuDoukou().IsConditionMet(hand))
                        {
                            handYaku.Add(new SanshokuDoukou());
                        }
                        if (new Shosangen().IsConditionMet(hand))
                        {
                            handYaku.Add(new Shosangen());
                        }
                        if (new Haku().IsConditionMet(hand))
                        {
                            handYaku.Add(new Haku());
                        }
                        if (new Hatsu().IsConditionMet(hand))
                        {
                            handYaku.Add(new Hatsu());
                        }
                        if (new Chun().IsConditionMet(hand))
                        {
                            handYaku.Add(new Chun());
                        }
                        if (new YakuhaiEast().IsConditionMet(hand, new object[]
                        {
                            config_.PlayerWind, config_.RoundWind
                        }))
                        {
                            if (config_.PlayerWind == EAST)
                            {
                                handYaku.Add(new YakuhaiOfPlace());
                            }
                            if (config_.RoundWind == EAST)
                            {
                                handYaku.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiSouth().IsConditionMet(hand, new object[]
                        {
                            config_.PlayerWind, config_.RoundWind
                        }))
                        {
                            if (config_.PlayerWind == SOUTH)
                            {
                                handYaku.Add(new YakuhaiOfPlace());
                            }
                            if (config_.RoundWind == SOUTH)
                            {
                                handYaku.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiWest().IsConditionMet(hand, new object[]
                        {
                            config_.PlayerWind, config_.RoundWind
                        }))
                        {
                            if (config_.PlayerWind == WEST)
                            {
                                handYaku.Add(new YakuhaiOfPlace());
                            }
                            if (config_.RoundWind == WEST)
                            {
                                handYaku.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new YakuhaiNorth().IsConditionMet(hand, new object[]
                        {
                            config_.PlayerWind, config_.RoundWind
                        }))
                        {
                            if (config_.PlayerWind == NORTH)
                            {
                                handYaku.Add(new YakuhaiOfPlace());
                            }
                            if (config_.RoundWind == NORTH)
                            {
                                handYaku.Add(new YakuhaiOfRound());
                            }
                        }
                        if (new Daisangen().IsConditionMet(hand))
                        {
                            handYaku.Add(new Daisangen());
                        }
                        if (new Shousuushii().IsConditionMet(hand))
                        {
                            handYaku.Add(new Shousuushii());
                        }
                        if (new DaiSuushi().IsConditionMet(hand))
                        {
                            if (config_.Options.HasDoubleYakuman)
                            {
                                handYaku.Add(new DaiSuushi());
                            }
                            else
                            {
                                handYaku.Add(new DaiSuushi { HanOpen = 13, HanClosed = 13 });
                            }
                        }
                        if (melds.Count() == 0 && new ChuurenPoutou().IsConditionMet(hand))
                        {
                            if (tiles34[winTile.Value / 4] == 2
                                || tiles34[winTile.Value / 4] == 4)
                            {
                                if (config_.Options.HasDoubleYakuman)
                                {
                                    handYaku.Add(new DaburuChuurenPoutou());
                                }
                                else
                                {
                                    handYaku.Add(new DaburuChuurenPoutou { HanClosed = 13 });
                                }
                            }
                            else
                            {
                                handYaku.Add(new ChuurenPoutou());
                            }
                        }
                        if (!isOpenHand && new Suuankou().IsConditionMet(hand, new object[]
                        {
                            winTile, config_.IsTsumo
                        }))
                        {
                            if (tiles34[winTile.Value / 4] == 2)
                            {
                                if (config_.Options.HasDoubleYakuman)
                                {
                                    handYaku.Add(new SuuankouTanki());
                                }
                                else
                                {
                                    handYaku.Add(new SuuankouTanki { HanClosed = 13 });
                                }
                            }
                            else
                            {
                                handYaku.Add(new Suuankou());
                            }
                        }
                        if (new SanKantsu().IsConditionMet(hand, new object[]
                        {
                            melds
                        }))
                        {
                            handYaku.Add(new SanKantsu());
                        }
                        if (new Suukantsu().IsConditionMet(hand, new object[]
                        {
                            melds
                        }))
                        {
                            handYaku.Add(new Suukantsu());
                        }
                    }

                    //役満に役満以外の役は付かない
                    var yakumanList = handYaku.Where(x => x.IsYakuman)
                                              .ToList();
                    if (yakumanList.Count != 0)
                    {
                        handYaku = yakumanList;
                    }

                    //翻を計算する
                    foreach (var item in handYaku)
                    {
                        if (isOpenHand && item.HanOpen != 0)
                        {
                            han += item.HanOpen;
                        }
                        else
                        {
                            han += item.HanClosed;
                        }
                    }
                    if (han == 0)
                    {
                        error = "There are no yaku in the hada.";
                        cost = null;
                    }

                    //役満にドラは付かない
                    if (yakumanList.Count == 0)
                    {
                        var tilesForDora = tiles.ToList();
                        foreach (var meld in melds)
                        {
                            if (meld.Type == MeldType.KAN || meld.Type == MeldType.CHANKAN)
                            {
                                tilesForDora.Add(meld.Tiles[3]);
                            }
                        }
                        var countOfDora = 0;
                        var countOfAkaDora = 0;
                        foreach (var tile in tilesForDora)
                        {
                            countOfDora += PlusDora(tile, doraIndicators);
                        }
                        foreach (var tile in tilesForDora)
                        {
                            if (IsAkaDora(tile, config_.Options.HasAkaDora))
                            {
                                countOfAkaDora++;
                            }
                        }
                        if (countOfDora != 0)
                        {
                            handYaku.Add(new Dora { HanOpen = countOfDora, HanClosed = countOfDora });
                            han += countOfDora;
                        }
                        if (countOfAkaDora != 0)
                        {
                            handYaku.Add(new Akadora { HanOpen = countOfAkaDora, HanClosed = countOfAkaDora });
                            han += countOfAkaDora;
                        }
                    }
                    if (string.IsNullOrEmpty(error))
                    {
                        cost = CalculateScores(han, fu, config_, yakumanList.Count > 0);
                    }
                    calculatedHands.Add(new HandResponse(
                        cost, han, fu, handYaku, error, fuDetails));
                }
            }
            if (!isOpenHand && new KokushiMusou().IsConditionMet(null, new object[] { tiles34 }))
            {
                if (tiles34[winTile.Value / 4] == 2)
                {
                    if (config_.Options.HasDoubleYakuman)
                    {
                        handYaku.Add(new DaburuKokushiMusou());
                    }
                    else
                    {
                        handYaku.Add(new DaburuKokushiMusou { HanClosed = 13 });
                    }
                }
                else
                {
                    handYaku.Add(new KokushiMusou());
                }
                if (config_.IsRenhou && config_.Options.RenhouAsYakuman)
                {
                    handYaku.Add(new RenhouYakuman());
                }
                if (config_.IsTenhou)
                {
                    handYaku.Add(new Tenhou());
                }
                if (config_.IsChiihou)
                {
                    handYaku.Add(new Chiihou());
                }
                var han = 0;
                foreach (var item in handYaku)
                {
                    if (isOpenHand && item.HanOpen != 0)
                    {
                        han += item.HanOpen;
                    }
                    else
                    {
                        han += item.HanClosed;
                    }
                }
                var fu = 0;
                var cost = CalculateScores(han, fu, config_, handYaku.Count > 0);
                calculatedHands.Add(new HandResponse(
                    cost, han, fu, handYaku, null, new List<FuDetail>()));
            }
            calculatedHands.Sort((x, y) =>
                x.Han < y.Han ? 1 : x.Han > y.Han ? -1
                : x.Fu < y.Fu ? 1 : x.Fu > y.Fu ? -1 : 0);
            return calculatedHands[0]; ;
        }

        private static IEnumerable<TileKinds> FindWinGroups(TileId winTile,
            IList<TileKinds> hand, IList<TileKinds> openMelds)
        {
            var winTile34 = winTile.ToTileKind();
            var closedSetItems = new List<TileKinds>();
            foreach (var x in hand)
            {
                if (!openMelds.Contains(x))
                {
                    closedSetItems.Add(x);
                }
                else
                {
                    openMelds.Remove(x);
                }
            }
            var winGroups = closedSetItems.Where(x => x.Contains(winTile34));
            return winGroups.Distinct();
        }

        private static int PlusDora(TileId tile, TileIds doraIndicators)
        {
            var tileIndex = tile.Value / 4;
            var doraCount = 0;
            foreach (var _dora in doraIndicators)
            {
                var dora = _dora.Value / 4;
                if (tileIndex < EAST)
                {
                    if (dora == 8) dora = -1;
                    if (dora == 17) dora = 8;
                    if (dora == 26) dora = 17;
                    if (tileIndex == dora + 1) doraCount++;
                }
                else
                {
                    if (dora < EAST) continue;
                    dora -= 9 * 3;
                    var tileIndexTemp = tileIndex - 9 * 3;
                    if (dora == 3) dora = -1;
                    if (dora == 6) dora = 3;
                    if (tileIndexTemp == dora + 1) doraCount++;
                }
            }
            return doraCount;
        }

        private static bool IsAkaDora(TileId tile, bool akaEnabled)
        {
            return akaEnabled
                && (tile.Value == FIVE_RED_MAN
                || tile.Value == FIVE_RED_PIN
                || tile.Value == FIVE_RED_SOU);
        }
    }
}