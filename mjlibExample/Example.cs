using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mjlibExample
{
    internal class Example
    {
        private static void Main(string[] args)
        {
            /***********************************************************************/
            /* タンヤオ　ロン                                                       */
            /***********************************************************************/
            var tiles = TileIds.Parse(man: "22444", pin: "333567", sou: "444");
            var winTile = TileId.Parse(sou: "4");
            var result = HandCalculator.EstimateHandValue(tiles, winTile);
            PrintHandResult(tiles, winTile, null, result);

            /***********************************************************************/
            /* タンヤオ　ツモ                                                       */
            /***********************************************************************/
            result = HandCalculator.EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            PrintHandResult(tiles, winTile, null, result);

            /***********************************************************************/
            /* 副露追加                                                             */
            /***********************************************************************/
            var melds = new List<Meld>
            {
                new Meld(MeldType.PON, tiles: TileIds.Parse(man: "444"))
            };
            var config = new HandConfig(options: new OptionalRules(hasOpenTanyao: true));
            result = HandCalculator.EstimateHandValue(tiles, winTile, melds, config: config);
            PrintHandResult(tiles, winTile, melds, result);

            /***********************************************************************/
            /* シャンテン数計算                                                             */
            /***********************************************************************/
            tiles = TileIds.Parse(man: "13569", pin: "123459", sou: "443");
            var shanten = Shanten.CalculateShanten(tiles);
            Console.WriteLine(tiles.ToOneLineString());
            Console.WriteLine($"{shanten}シャンテン");
            Console.WriteLine("");

            /***********************************************************************/
            /* 数え役満                                                             */
            /***********************************************************************/
            //13翻打ち止め
            tiles = TileIds.Parse(man: "22244466677788");
            winTile = TileId.Parse(man: "7");
            melds = new List<Meld>
            {
                new Meld(MeldType.KAN, TileIds.Parse(man: "2222"), opened: false)
            };
            var doraIndicators = TileIds.Parse(man: "1111"); //ドラ表示牌
            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Limited));
            result = HandCalculator.EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            PrintHandResult(tiles, winTile, melds, result);

            //三倍満扱い
            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Sanbaiman));
            result = HandCalculator.EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            PrintHandResult(tiles, winTile, melds, result);

            //13翻ごとに役満を重ねる(26翻でダブル役満)
            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Nolimit));
            result = HandCalculator.EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            PrintHandResult(tiles, winTile, melds, result);
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