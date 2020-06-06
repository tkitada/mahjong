﻿using System;
using static mjlib.HandCalculating.HandConstans;

namespace mjlib.HandCalculating
{
    /// <summary>
    /// Main: 上がった人の点数
    /// Additional: それ以外の人の点数
    /// </summary>
    internal class Cost
    {
        public int Main { get; }
        public int Additional { get; }

        public Cost(int main, int additional)
        {
            Main = main;
            Additional = additional;
        }
    }

    internal class ScoresCalcurator
    {
        public Cost CalculateScores(int han, int fu, HandConfig config, bool isYakuman = false)
        {
            //数え役満
            if (han >= 13 && !isYakuman)
            {
                if (config.Options.KazoeLimit == KAZOE_LIMITED)
                {
                    han = 13;
                }
                else if (config.Options.KazoeLimit == KAZOE_SANBAIMAN)
                {
                    han = 12;
                }
            }

            //rounded: 一家あたりの点数 跳満(子)-12000なら3000点
            int rounded, doubleRounded, fourRounded, sixRounded;
            if (han >= 5)
            {
                if (han >= 78)
                {
                    rounded = 48000;
                }
                else if (han >= 65)
                {
                    rounded = 40000;
                }
                else if (han >= 52)
                {
                    rounded = 32000;
                }
                else if (han >= 39)
                {
                    rounded = 24000;
                }
                //ダブル役満
                else if (han >= 26)
                {
                    rounded = 16000;
                }
                //役満
                else if (han >= 13)
                {
                    rounded = 8000;
                }
                //三倍満
                else if (han >= 11)
                {
                    rounded = 6000;
                }
                //倍満
                else if (han >= 8)
                {
                    rounded = 4000;
                }
                //跳満
                else if (han >= 6)
                {
                    rounded = 3000;
                }
                else
                {
                    rounded = 2000;
                }
                doubleRounded = rounded * 2;
                fourRounded = doubleRounded * 2;
                sixRounded = doubleRounded * 3;
            }
            else
            {
                var basePoint = fu * Math.Pow(2, 2 + han);
                rounded = (int)((basePoint + 99) / 100) * 100;
                doubleRounded = (int)((2 * basePoint + 99) / 100) * 100;
                fourRounded = (int)((4 * basePoint + 99) / 100) * 100;
                sixRounded = (int)((6 * basePoint + 99) / 100) * 100;

                var IsKiriage = false;
                if (config.Options.Kiriage)
                {
                    if (han == 4 && fu == 30)
                    {
                        IsKiriage = true;
                    }
                    if (han == 3 && fu == 60)
                    {
                        IsKiriage = true;
                    }
                }

                //満貫
                if (rounded > 2000 || IsKiriage)
                {
                    rounded = 2000;
                    doubleRounded = rounded * 2;
                    fourRounded = doubleRounded * 2;
                    sixRounded = doubleRounded * 3;
                }
            }

            return config.IsTsumo
                ? new Cost(doubleRounded, config.IsDealer ? doubleRounded : rounded)
                : new Cost(config.IsDealer ? sixRounded : fourRounded, 0);
        }
    }
}