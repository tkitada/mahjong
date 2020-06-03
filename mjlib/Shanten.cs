﻿using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace mjlib
{
    internal class Shanten
    {
        private const int AGARI_STATE = -1;

        private TilesSet tiles_ = new TilesSet();
        private int numberMelds_ = 0;
        private int numberTatsu_ = 0;
        private int numberPairs_ = 0;
        private int numberJidahai_ = 0;
        private int numberCharacters_ = 0;
        private int numberIsolatedTiles_ = 0;
        private int minShanten_ = 0;

        public int CalculateShanten(TilesSet _tilesSet, List<Tiles34> openSets,
            bool chiitoitsu = true, bool kokushi = true)
        {
            var tilesSet = new TilesSet(_tilesSet.Select(t => t).ToList());
            Init(tilesSet);
            var countOfTiles = tilesSet.Sum();
            if (countOfTiles > 14) return -2;

            if (openSets.Count != 0)
            {
                var isolatedTiles = tiles_.FindIsolatedTileIndices();
                foreach (var meld in openSets)
                {
                    if (isolatedTiles.Count == 0) break;

                    var lastIndex = isolatedTiles.Count - 1;
                    var isolatedTile = isolatedTiles[lastIndex];
                    isolatedTiles.RemoveAt(lastIndex);

                    tilesSet[meld[0].Value] -= 1;
                    tilesSet[meld[1].Value] -= 1;
                    tilesSet[meld[2].Value] -= 1;
                    tilesSet[isolatedTile.Value] = 3;
                }
            }

            if (openSets.Count == 0)
            {
                minShanten_ = ScanChiitoitsuAndKokushi(chiitoitsu, kokushi);
            }

            RemoveCharacterTiles(countOfTiles);

            var initMentsu = (14 - countOfTiles) / 3;
            Scan(initMentsu);
            return minShanten_;
        }

        private void Init(TilesSet tiles)
        {
            tiles_ = tiles;
            numberMelds_ = 0;
            numberTatsu_ = 0;
            numberPairs_ = 0;
            numberJidahai_ = 0;
            numberCharacters_ = 0;
            numberIsolatedTiles_ = 0;
            minShanten_ = 8;
        }

        private int ScanChiitoitsuAndKokushi(bool chiitoitsu, bool kokushi)
        {
            var shanten = minShanten_;
            var yaochuIndices = new List<int> { 0, 8, 9, 17, 18,
                26, 27, 28, 29, 30, 31, 32, 33 };

            var completedTerminals = 0;
            foreach (var i in yaochuIndices)
            {
                completedTerminals += tiles_[i] >= 2 ? 1 : 0;
            }

            var terminals = 0;
            foreach (var i in yaochuIndices)
            {
                terminals += tiles_[i] != 0 ? 1 : 0;
            }

            var chuchanIndices = new List<int> { 1, 2, 3, 4, 5, 6, 7,
                10, 11, 12, 13, 14, 15, 16,
                19, 20, 21, 22, 23, 24, 25 };

            var completedPairs = completedTerminals;
            foreach (var i in chuchanIndices)
            {
                completedPairs += tiles_[i] >= 2 ? 1 : 0;
            }

            var pairs = terminals;
            foreach (var i in chuchanIndices)
            {
                pairs += tiles_[i] != 0 ? 1 : 0;
            }

            if (chiitoitsu)
            {
                var retShanten = 6 - completedPairs + pairs < 7 ? 7 - pairs : 0;
                if (retShanten < shanten)
                {
                    shanten = retShanten;
                }
            }
            if (kokushi)
            {
                var retShanten = 13 - terminals - completedTerminals != 0 ? 1 : 0;
                if (retShanten < shanten)
                {
                    shanten = retShanten;
                }
            }
            return shanten;
        }

        private void RemoveCharacterTiles(int CountOfTiles)
        {
            var number = 0;
            var isoleted = 0;
            for (var i = 27; i < 34; i++)
            {
                if (tiles_[i] == 4)
                {
                    numberMelds_ += 1;
                    numberJidahai_ += 1;
                    number |= 1 << (i - 27);
                    isoleted |= 1 << (i - 27);
                }
                if (tiles_[i] == 3)
                {
                    numberMelds_ += 1;
                }
                if (tiles_[i] == 2)
                {
                    numberPairs_ += 1;
                }
                if (tiles_[i] == 1)
                {
                    isoleted |= i << (i - 27);
                }
            }
            if (numberJidahai_ != 0 && (CountOfTiles % 3) == 2)
            {
                numberJidahai_ -= 1;
            }
            if (isoleted != 0)
            {
                numberIsolatedTiles_ |= 1 << 27;
                if ((number | isoleted) == number)
                {
                    numberCharacters_ |= 1 << 27;
                }
            }
        }

        private void Scan(int initMentsu)
        {
            numberCharacters_ = 0;
            for (var i = 0; i < 27; i++)
            {
                numberCharacters_ |= tiles_[i] == 4 ? 1 << i : 0;
            }
            numberMelds_ += initMentsu;
            Run(0);
        }

        private void Run(int depth)
        {
            if (minShanten_ == AGARI_STATE) return;

            while (tiles_[depth] == 0)
            {
                depth += 1;
                if (depth >= 27) break;
            }
            if (depth >= 27)
            {
                UpdateResult();
                return;
            }
            var i = depth;
            if (i > 8)
            {
                i -= 9;
            }
            if (i > 8)
            {
                i -= 9;
            }
            if (tiles_[depth] == 4)
            {
                IncreaseSet(depth);
                if (i < 7 && tiles_[depth + 2] != 0)
                {
                    if (tiles_[depth + 1] != 0)
                    {
                        IncreaseSyuntsu(depth);
                        Run(depth + 1);
                        DecreaseSyuntsu(depth);
                    }
                    IncreaseTatsuSecond(depth);
                    Run(depth + 1);
                    DecreaseTatsuSecond(depth);
                }
                if (i < 8 && tiles_[depth + 1] != 0)
                {
                    IncreaseTatsuFirst(depth);
                    Run(depth + 1);
                    DecreaseTatsuSecond(depth);
                }
                IncreaseIsolatedTile(depth);
                Run(depth + 1);
                DecreaseIsolatedTile(depth);
                DecreaseSet(depth);
                IncreasePair(depth);

                if (i < 7 && tiles_[depth + 2] != 0)
                {
                    if (tiles_[depth + 1] != 0)
                    {
                        IncreaseSyuntsu(depth);
                        Run(depth);
                        DecreaseSyuntsu(depth);
                    }
                    IncreaseTatsuSecond(depth);
                    Run(depth + 1);
                    DecreaseTatsuSecond(depth);
                }
                if (i < 8 && tiles_[depth + 1] != 0)
                {
                    IncreaseTatsuFirst(depth);
                    Run(depth + 1);
                    DecreaseTatsuFirst(depth);
                }
                DecreasePair(depth);
            }
            if (tiles_[depth] == 3)
            {
                IncreaseSet(depth);
                Run(depth + 1);
                DecreaseSet(depth);
                IncreasePair(depth);
                if (i < 7 && tiles_[depth + 1] != 0 && tiles_[depth + 2] != 0)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth + 1);
                    DecreaseSyuntsu(depth);
                }
                else
                {
                    if (i < 7 && tiles_[depth + 2] != 0)
                    {
                        IncreaseTatsuSecond(depth);
                        Run(depth + 1);
                        DecreaseTatsuSecond(depth);
                    }
                    if (i < 8 && tiles_[depth + 1] != 0)
                    {
                        IncreaseTatsuFirst(depth);
                        Run(depth + 1);
                        DecreaseTatsuFirst(depth);
                    }
                }
                DecreasePair(depth);

                if (i < 7 && tiles_[depth + 2] >= 2 && tiles_[depth + 1] >= 2)
                {
                    IncreaseSyuntsu(depth);
                    IncreaseSyuntsu(depth);
                    Run(depth);
                    DecreaseSyuntsu(depth);
                    DecreaseSyuntsu(depth);
                }
            }
            if (tiles_[depth] == 2)
            {
                IncreasePair(depth);
                Run(depth + 1);
                DecreasePair(depth);
                if (1 < 7 && tiles_[depth + 2] != 0 && tiles_[depth + 1] != 0)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth);
                    DecreaseSyuntsu(depth);
                }
            }
            if (tiles_[depth] == 1)
            {
                if (i < 6 && tiles_[depth + 1] == 1
                    && tiles_[depth + 2] != 0 && tiles_[depth + 3] != 4)
                {
                    IncreaseSyuntsu(depth);
                    Run(depth + 2);
                    DecreaseSyuntsu(depth);
                }
                else
                {
                    IncreaseIsolatedTile(depth);
                    Run(depth + 1);
                    DecreaseIsolatedTile(depth);
                    if (1 < 7 && tiles_[depth + 2] != 0)
                    {
                        if (tiles_[depth + 1] != 0)
                        {
                            IncreaseSyuntsu(depth);
                            Run(depth + 1);
                            DecreaseSyuntsu(depth);
                        }
                        IncreaseTatsuSecond(depth);
                        Run(depth + 1);
                        DecreaseTatsuSecond(depth);
                    }
                    if (i < 8 && tiles_[depth + 1] != 0)
                    {
                        IncreaseTatsuFirst(depth);
                        Run(depth + 1);
                        DecreaseTatsuFirst(depth);
                    }
                }
            }
        }

        private void UpdateResult()
        {
            var retShanten = 8 - numberMelds_ * 2 - numberTatsu_ - numberPairs_;
            var nMentsuKouho = numberPairs_ + numberTatsu_;
            if (numberPairs_ != 0)
            {
                nMentsuKouho += numberPairs_ - 1;
            }
            else if (numberCharacters_ != 0 && numberIsolatedTiles_ != 0)
            {
                if ((numberCharacters_ | numberIsolatedTiles_) == numberCharacters_)
                {
                    retShanten += 1;
                }
            }
            if (nMentsuKouho > 4)
            {
                retShanten += nMentsuKouho - 4;
            }
            if (retShanten != AGARI_STATE && retShanten < numberJidahai_)
            {
                retShanten = numberJidahai_;
            }
            if (retShanten < minShanten_)
            {
                minShanten_ = retShanten;
            }
        }

        private void IncreaseSet(int k)
        {
            tiles_[k] -= 3;
            numberMelds_ += 1;
        }

        private void DecreaseSet(int k)
        {
            tiles_[k] += 3;
            numberMelds_ -= 1;
        }

        private void IncreasePair(int k)
        {
            tiles_[k] -= 2;
            numberPairs_ += 1;
        }

        private void DecreasePair(int k)
        {
            tiles_[k] += 2;
            numberPairs_ -= 1;
        }

        private void IncreaseSyuntsu(int k)
        {
            tiles_[k] -= 1;
            tiles_[k + 1] -= 1;
            tiles_[k + 2] -= 1;
            numberMelds_ += 1;
        }

        private void DecreaseSyuntsu(int k)
        {
            tiles_[k] -= 1;
            tiles_[k + 1] -= 1;
            tiles_[k + 2] -= 1;
            numberMelds_ -= 1;
        }

        private void IncreaseTatsuFirst(int k)
        {
            tiles_[k] -= 1;
            tiles_[k + 1] -= 1;
            numberTatsu_ += 1;
        }

        private void DecreaseTatsuFirst(int k)
        {
            tiles_[k] += 1;
            tiles_[k + 1] += 1;
            numberTatsu_ -= 1;
        }

        private void IncreaseTatsuSecond(int k)
        {
            tiles_[k] -= 1;
            tiles_[k + 2] -= 1;
            numberTatsu_ += 1;
        }

        private void DecreaseTatsuSecond(int k)
        {
            tiles_[k] += 1;
            tiles_[k + 2] += 1;
            numberTatsu_ -= 1;
        }

        private void IncreaseIsolatedTile(int k)
        {
            tiles_[k] -= 1;
            numberIsolatedTiles_ |= 1 << k;
        }

        private void DecreaseIsolatedTile(int k)
        {
            tiles_[k] += 1;
            numberIsolatedTiles_ |= 1 << k;
        }
    }
}