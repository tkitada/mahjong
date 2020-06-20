using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.Constants;
using static mjlib.HandCalculating.FuCalculator;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class FuCalculationTest
    {
        [TestMethod]
        public void ChitoitsuFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "115599", pin: "6", sou: "112244");
            var winTile = TileId.Parse(pin: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(25, BASE)));
            AreEqual(fu, 25);
        }

        [TestMethod]
        public void OpenHandBaseTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON,sou:"222")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, OPEN_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void FuBasedOnWinGroup()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "234789", pin: "1234566");
            var winTile = TileId.Parse(pin: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var winGroups = hand.Where(x => x.Contains(winTile.ToTileKind())).ToList();

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                winGroups[0],
                config);
            AreEqual(30, fu);

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                winGroups[1],
                config);
            AreEqual(40, fu);
        }

        [TestMethod]
        public void OpenHandWithoutAdditionalFu()
        {
            var config = new HandConfig(options: new OptionalRules(fuForOpenPinfu: false));

            var tiles = TileIds.Parse(man: "234567", pin: "22", sou: "23478");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.CHI,sou:"234")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            AreEqual(fu, 20);
        }

        [TestMethod]
        public void TsumoHandBaseTest()
        {
            var config = new HandConfig(isTsumo: true);

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, _) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
        }

        [TestMethod]
        public void TsumoHandAndPinfuTest()
        {
            var config = new HandConfig(isTsumo: true);

            var tiles = TileIds.Parse(man: "123456", pin: "123", sou: "2278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            AreEqual(fu, 20);
        }

        [TestMethod]
        public void TsumoAndDisabledPinfuTest()
        {
            var config = new HandConfig(isTsumo: true,
                options: new OptionalRules(fuForPinfuTsumo: true));

            var tiles = TileIds.Parse(man: "123456", pin: "123", sou: "2278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, TSUMO)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void TsumoHandAndNotPinfu()
        {
            var config = new HandConfig(isTsumo: true);

            var tiles = TileIds.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, TSUMO)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void PenchanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "55", sou: "12456");
            var winTile = TileId.Parse(sou: "3");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PENCHAN)));
            AreEqual(fu, 40);

            tiles = TileIds.Parse(man: "123456", pin: "55", sou: "34589");
            winTile = TileId.Parse(sou: "7");
            hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PENCHAN)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void KanChanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "55", sou: "12357");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, KANCHAN)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ValuedPairFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", sou: "12378", honors: "11");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var valuedTiles = new List<int> { EAST };
            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                valuedTiles: valuedTiles);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            AreEqual(fu, 40);

            valuedTiles = new List<int> { EAST, EAST };
            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                valuedTiles: valuedTiles);

            AreEqual(3, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void PairWaitFu()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "1", sou: "123678");
            var winTile = TileId.Parse(pin: "1");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PAIR_WAIT)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, CLOSED_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedTerminalPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "11178");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, CLOSED_TERMINAL_PON)));
            AreEqual(fu, 40);

            tiles = TileIds.Parse(man: "123456", pin: "11", sou: "11678");
            winTile = TileId.Parse(sou: "1");
            hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedHonorPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", sou: "1178", honors: "111");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, CLOSED_TERMINAL_PON)));
            AreEqual(fu, 40);

            tiles = TileIds.Parse(man: "123456", sou: "11678", honors: "11");
            winTile = TileId.Parse(honors: "1");
            hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void OpenPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", sou: "22278", pin: "11");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON,sou:"222")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, OPEN_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void OpenTerminalPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", sou: "2278", honors: "111");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON,honors:"111")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void ClosedKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, sou:"222", isOpen:false)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(16, CLOSED_KAN)));
            AreEqual(fu, 50);
        }

        [TestMethod]
        public void OpenKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, sou:"222", isOpen:true)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, OPEN_KAN)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void ClosedTerminalKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, pin:"111", isOpen:false)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(32, CLOSED_TERMINAL_KAN)));
            AreEqual(fu, 70);
        }

        [TestMethod]
        public void OpenTerminalKanFu()
        {
            var config = new HandConfig();

            var tiles = TileIds.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = TileId.Parse(sou: "6");
            var hand = Hand(new TileIds(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, pin:"111", isOpen:true)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(16, OPEN_TERMINAL_KAN)));
            AreEqual(fu, 40);
        }

        private TileKinds GetWinGroup(IList<TileKinds> hand, TileId winTile)
        {
            return hand.Where(x => x.Contains(winTile.ToTileKind())).ToList()[0];
        }
    }
}