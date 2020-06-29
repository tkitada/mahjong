using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using Simple.Common.Models;
using Simple.Player.Application;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Player.Console
{
    internal class ViewModel
    {
        public TileIds Hand { get; set; }

        private readonly PlayerApplicationService appService_;

        public ViewModel()
        {
            appService_ = new PlayerApplicationService("yamada");

            appService_.RequestJoin();

            appService_.JoinEvent += (_, e) =>
            {
                System.Console.WriteLine($"id: {e.JoinRes.Id}");
                appService_.RequestHand();
            };
            appService_.HandEvent += (_, e) =>
            {
                Hand = e.HandRes.Hand;
                System.Console.WriteLine($"hand: {Hand.ToOneLineString()}");
                appService_.RequestTsumo();
            };
            appService_.TsumoEvent += (_, e) =>
            {
                var tsumo = e.TsumoRes.Tsumo;
                System.Console.WriteLine($"tsumo: {tsumo.ToOneLineString()}");
                Hand.Add(tsumo);
                System.Console.WriteLine($"shanten: {Shanten.CalculateShanten(Hand)}");
                var result = HandCalculator.EstimateHandValue(Hand, tsumo, config: new HandConfig(isTsumo: true));
                if (result.Error is null)
                {
                    appService_.RequestAgari();
                    return;
                }
                var index = DecideDahai();
                Hand.RemoveAt(index);
                appService_.RequestDahai(index);
            };
            appService_.DahaiEvent += (_, e) => appService_.RequestTsumo();
            appService_.AgariEvent += (_, e) =>
            {
                var res = e.AgariRes;
                PrintHandResult(res.Tiles, res.WinTile, res.Melds, res.Result);
            };
        }

        private int DecideDahai()
        {
            var shantenList = new List<int>();
            for (var i = 0; i < Hand.Count; i++)
            {
                var h = new TileIds(Hand);
                h.RemoveAt(i);
                shantenList.Add(Shanten.CalculateShanten(h));
            }
            return shantenList.IndexOf(shantenList.Min());
        }

        private static void PrintHandResult(TileIds tiles, TileId winTile, List<Meld> melds, HandResponseModel result)
        {
            System.Console.WriteLine($"{tiles.ToOneLineString()}");
            var IsOpened = false;
            if (melds is null)
            {
                System.Console.WriteLine("鳴きなし");
            }
            else
            {
                foreach (var meldItem in melds)
                {
                    System.Console.WriteLine(meldItem);
                }
                IsOpened = melds.Count(x => x.Opened) > 0;
            }
            System.Console.WriteLine($"和了牌: {new TileIds(new List<TileId> { winTile }).ToOneLineString()}");
            foreach (var yakuItem in result.Yaku)
            {
                var han = IsOpened ? yakuItem.HanOpen : yakuItem.HanClosed;
                System.Console.WriteLine($"{yakuItem.Japanese}\t{han}翻");
            }
            System.Console.WriteLine($"{result.Han}翻 {result.Fu}符");
            System.Console.WriteLine($"{result.Cost.Main}点");
            foreach (var fuItem in result.FuDetailSet)
            {
                System.Console.WriteLine($"符: {fuItem.Fu}\tReason: {fuItem.Reason}");
            }
            System.Console.WriteLine("");
        }
    }
}