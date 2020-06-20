using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.HandCalculating;
using mjlib.HandCalculating.YakuList;
using mjlib.Tiles;
using System.Collections.Generic;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.Constants;
using static mjlib.HandCalculating.HandCalculator;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class YakuCalculationTest
    {
        [TestMethod]
        public void HandCalculationTest()
        {
            var playerWind = EAST;

            var tiles = TileIds.Parse(pin: "112233999", honors: "11177");
            var winTile = TileId.Parse(pin: "9");
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, honors:"111"),
                MakeMeld(MeldType.CHI, pin:"123"),
                MakeMeld(MeldType.CHI, pin:"123")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);

            tiles = TileIds.Parse(pin: "22244456799", honors: "444");
            winTile = TileId.Parse(pin: "2");
            var doraIndicators = TileIds.Parse(sou: "3", honors: "3");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, honors:"4444")
            };
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators);
            AreEqual(null, result.Error);
            AreEqual(6, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "11", pin: "123345", sou: "678", honors: "666");
            winTile = TileId.Parse(pin: "3");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);

            tiles = TileIds.Parse(man: "234789", pin: "12345666");
            winTile = TileId.Parse(pin: "6");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(30, result.Fu);

            tiles = TileIds.Parse(pin: "34555789", sou: "678", honors: "555");
            winTile = TileId.Parse(pin: "5");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);

            tiles = TileIds.Parse(man: "678", pin: "88", sou: "123345678");
            winTile = TileId.Parse(sou: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(man: "123456", pin: "456", sou: "12399");
            winTile = TileId.Parse(sou: "1");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(sou: "111123666789", honors: "11");
            winTile = TileId.Parse(sou: "1");
            melds = new List<Meld> { MakeMeld(MeldType.PON, sou: "666") };
            doraIndicators = TileIds.Parse(honors: "4");
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, new HandConfig(playerWind: playerWind));
            AreEqual(40, result.Fu);
            AreEqual(4, result.Han);

            tiles = TileIds.Parse(pin: "12333", sou: "567", honors: "666777");
            winTile = TileId.Parse(pin: "3");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, honors:"666"),
                MakeMeld(MeldType.PON, honors:"777")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(man: "456", pin: "12367778", sou: "678");
            winTile = TileId.Parse(pin: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true));
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileIds.Parse(man: "11156677899", honors: "777");
            winTile = TileId.Parse(man: "7");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, honors:"7777"),
                MakeMeld(MeldType.PON, man:"111"),
                MakeMeld(MeldType.CHI, man:"678")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(40, result.Fu);
            AreEqual(3, result.Han);

            tiles = TileIds.Parse(man: "122223777888", honors: "66");
            winTile = TileId.Parse(man: "2");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.CHI, man:"123"),
                MakeMeld(MeldType.PON, man:"777")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(pin: "11144678888", honors: "444");
            winTile = TileId.Parse(pin: "8");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, honors:"444"),
                MakeMeld(MeldType.PON, pin:"111"),
                MakeMeld(MeldType.PON, pin:"888")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(man: "345", pin: "999", sou: "67778", honors: "222");
            winTile = TileId.Parse(sou: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileIds.Parse(man: "345", sou: "33445577789");
            winTile = TileId.Parse(sou: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(pin: "112233667788", honors: "22");
            winTile = TileId.Parse(pin: "3");
            melds = new List<Meld> { MakeMeld(MeldType.CHI, pin: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(man: "12333456789", sou: "345");
            winTile = TileId.Parse(man: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(sou: "11123456777888");
            winTile = TileId.Parse(sou: "4");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.CHI, sou:"123"),
                MakeMeld(MeldType.PON, sou:"777"),
                MakeMeld(MeldType.PON, sou:"888")
            };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(5, result.Han);

            tiles = TileIds.Parse(sou: "112233789", honors: "55777");
            winTile = TileId.Parse(sou: "2");
            melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(40, result.Fu);
            AreEqual(4, result.Han);

            tiles = TileIds.Parse(pin: "234777888999", honors: "22");
            winTile = TileId.Parse(pin: "9");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.CHI, pin:"234"),
                MakeMeld(MeldType.CHI, pin:"789")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileIds.Parse(man: "444", pin: "77888899", honors: "777");
            winTile = TileId.Parse(pin: "8");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, honors:"777"),
                MakeMeld(MeldType.PON, man:"444")
            };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileIds.Parse(man: "567", pin: "12333345", honors: "555");
            winTile = TileId.Parse(pin: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileIds.Parse(pin: "34567777889", honors: "555");
            winTile = TileId.Parse(pin: "7");
            melds = new List<Meld> { MakeMeld(MeldType.CHI, pin: "345") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(3, result.Han);

            tiles = TileIds.Parse(pin: "567", sou: "333444555", honors: "77");
            winTile = TileId.Parse(sou: "3");
            melds = new List<Meld> { MakeMeld(MeldType.KAN, isOpen: false, sou: "4444") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isRiichi: true));
            AreEqual(60, result.Fu);
            AreEqual(1, result.Han);
        }

        [TestMethod]
        public void IsRiichiTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isRiichi: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsTsumoTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsIppatsuTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true, isIppatsu: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);

            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: false, isIppatsu: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsRinshanTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRinshan: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChankanTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isChankan: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHaiteiTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isHaitei: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHouteiTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isHoutei: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsRenhouTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRenhou: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsDaburuRiichiTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isDaburuRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsNagashiManganTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "13579");
            var result = EstimateHandValue(tiles, null, config: new HandConfig(isNagashiMangan: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChitoitsuHandTest()
        {
            var tiles = Tiles34.Parse(man: "113355", pin: "11", sou: "113355");
            IsTrue(new Chiitoitsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "2299", pin: "1199", sou: "2299", honors: "44");
            IsTrue(new Chiitoitsu().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "113355", pin: "11", sou: "113355");
            var winTile = TileId.Parse(pin: "1");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(25, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsTanyaoTest()
        {
            var tiles = Tiles34.Parse(man: "234567", pin: "22", sou: "234567");
            IsTrue(new Tanyao().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "234567", pin: "22", sou: "123456");
            IsFalse(new Tanyao().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "234567", sou: "234567", honors: "22");
            IsFalse(new Tanyao().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "234567", pin: "22", sou: "234567");
            var winTile = TileId.Parse(man: "7");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(3, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "234567", pin: "22", sou: "234567");
            winTile = TileId.Parse(man: "7");
            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "234") };
            result = EstimateHandValue(
                tiles_, winTile, melds, config: new HandConfig(options: new OptionalRules(hasOpenTanyao: true)));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "234567", pin: "22", sou: "234567");
            winTile = TileId.Parse(man: "7");
            melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "234") };
            result = EstimateHandValue(
                tiles_, winTile, melds, config: new HandConfig(options: new OptionalRules(hasOpenTanyao: false)));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsPinfuHandTest()
        {
            var playerWind = EAST;
            var roundWind = WEST;

            var tiles = TileIds.Parse(man: "123456", pin: "55", sou: "123456");
            var winTile = TileId.Parse(man: "6");
            var result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(man: "123456", pin: "55", sou: "123555");
            winTile = TileId.Parse(man: "5");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", pin: "55", sou: "111456");
            winTile = TileId.Parse(man: "6");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", pin: "55", sou: "123456");
            winTile = TileId.Parse(sou: "3");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", pin: "55", sou: "123567");
            winTile = TileId.Parse(sou: "6");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "22456678", pin: "123678");
            winTile = TileId.Parse(man: "2");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", sou: "123678", honors: "11");
            winTile = TileId.Parse(sou: "6");
            result = EstimateHandValue(
                tiles, winTile, config: new HandConfig(playerWind: playerWind, roundWind: roundWind));
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", sou: "123678", honors: "22");
            winTile = TileId.Parse(sou: "6");
            result = EstimateHandValue(
                tiles, winTile, config: new HandConfig(playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(man: "123456", pin: "456", sou: "12399");
            winTile = TileId.Parse(man: "1");
            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsIipeikoTest()
        {
            var tiles = Tiles34.Parse(man: "123", pin: "23444", sou: "112233");
            IsTrue(new Iipeiko().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "333", pin: "12344", sou: "112233");
            var winTile = TileId.Parse(man: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsRyanpeikouTest()
        {
            var tiles = Tiles34.Parse(man: "22", pin: "223344", sou: "112233");
            IsTrue(new Ryanpeikou().IsConditionMet(Hand(tiles, 1)));

            tiles = Tiles34.Parse(man: "22", sou: "111122223333");
            IsTrue(new Ryanpeikou().IsConditionMet(Hand(tiles, 1)));

            tiles = Tiles34.Parse(man: "123", pin: "23444", sou: "112233");
            IsFalse(new Ryanpeikou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "33", pin: "223344", sou: "112233");
            var winTile = TileId.Parse(pin: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsSanshokuTest()
        {
            var tiles = Tiles34.Parse(man: "123", pin: "12345677", sou: "123");
            IsTrue(new Sanshoku().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "23455", pin: "123", sou: "123456");
            IsFalse(new Sanshoku().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "12399", pin: "123", sou: "123456");
            var winTile = TileId.Parse(man: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsSanshokuDoukouTest()
        {
            var tiles = Tiles34.Parse(man: "111", pin: "11145677", sou: "111");
            IsTrue(new SanshokuDoukou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "222", pin: "33344455", sou: "111");
            IsFalse(new SanshokuDoukou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "222", pin: "22245699", sou: "222");
            var winTile = TileId.Parse(pin: "9");
            var melds = new List<Meld> { MakeMeld(MeldType.PON, sou: "222") };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsToitoiTest()
        {
            var tiles = Tiles34.Parse(man: "333", pin: "44555", sou: "111333");
            IsTrue(new Toitoi().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(pin: "777888999", sou: "777", honors: "44");
            IsTrue(new Toitoi().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "333", pin: "44555", sou: "111333");
            var winTile = TileId.Parse(pin: "5");
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, sou: "111"),
                MakeMeld(MeldType.PON, sou: "333")
            };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(pin: "777888999", sou: "777", honors: "44");
            winTile = TileId.Parse(pin: "9");
            melds = new List<Meld> { MakeMeld(MeldType.PON, sou: "777") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsSankantsuTest()
        {
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, sou: "1111"),
                MakeMeld(MeldType.KAN, sou: "3333"),
                MakeMeld(MeldType.KAN, pin: "6666")
            };
            IsTrue(new SanKantsu().IsConditionMet(null, new object[] { melds }));

            var tiles = TileIds.Parse(man: "123", pin: "44666", sou: "111333");
            var winTile = TileId.Parse(man: "3");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.CHANKAN, sou: "1111"),
                MakeMeld(MeldType.KAN, sou: "3333"),
                MakeMeld(MeldType.KAN, pin: "6666")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHonrotoTest()
        {
            var tiles = Tiles34.Parse(man: "111", sou: "111999", honors: "11222");
            IsTrue(new Honroto().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "1199", pin: "11", honors: "22334466");
            IsTrue(new Honroto().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "111", sou: "111999", honors: "11222");
            var winTile = TileId.Parse(honors: "2");
            var melds = new List<Meld> { MakeMeld(MeldType.PON, sou: "111") };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "1199", pin: "11", honors: "22334466");
            winTile = TileId.Parse(man: "1");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(4, result.Han);
            AreEqual(25, result.Fu);
        }

        [TestMethod]
        public void IsSanankouTest()
        {
            var tiles = Tiles34.Parse(man: "333", pin: "44555", sou: "111444");
            var winTile = TileId.Parse(sou: "4");
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, sou: "111"),
                MakeMeld(MeldType.PON, sou: "444")
            };
            IsFalse(new Sanankou().IsConditionMet(Hand(tiles), new object[] { winTile, melds, false }));

            melds = new List<Meld> { MakeMeld(MeldType.PON, sou: "111") };
            IsFalse(new Sanankou().IsConditionMet(Hand(tiles), new object[] { winTile, melds, false }));
            IsTrue(new Sanankou().IsConditionMet(Hand(tiles), new object[] { winTile, melds, true }));

            tiles = Tiles34.Parse(pin: "444789999", honors: "22333");
            winTile = TileId.Parse(pin: "9");
            IsTrue(new Sanankou().IsConditionMet(Hand(tiles), new object[] { winTile, new List<Meld>(), false }));

            melds = new List<Meld> { MakeMeld(MeldType.CHI, pin: "456") };
            tiles = Tiles34.Parse(pin: "222456666777", honors: "77");
            winTile = TileId.Parse(pin: "6");
            IsFalse(new Sanankou().IsConditionMet(Hand(tiles), new object[] { winTile, melds, false }));

            var tiles_ = TileIds.Parse(man: "333", pin: "44555", sou: "123444");
            melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            winTile = TileId.Parse(pin: "5");
            var result = EstimateHandValue(tiles_, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsShosangenTest()
        {
            var tiles = Tiles34.Parse(man: "345", sou: "123", honors: "55666777");
            IsTrue(new Shosangen().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "345", sou: "123", honors: "55666777");
            var winTile = TileId.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(3, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChantaTest()
        {
            var tiles = Tiles34.Parse(man: "123789", sou: "123", honors: "22333");
            IsTrue(new Chanta().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111", honors: "22333");
            IsFalse(new Chanta().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111999", pin: "11999");
            IsFalse(new Chanta().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "123789", sou: "123", honors: "22333");
            var winTile = TileId.Parse(honors: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsJunchanTest()
        {
            var tiles = Tiles34.Parse(man: "123789", pin: "12399", sou: "789");
            IsTrue(new Junchan().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111", honors: "22333");
            IsFalse(new Junchan().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", pin: "11999", sou: "111999");
            IsFalse(new Junchan().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "123789", pin: "12399", sou: "789");
            var winTile = TileId.Parse(man: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "789") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHonitsuTest()
        {
            var tiles = Tiles34.Parse(man: "123456789", honors: "11122");
            IsTrue(new Honitsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "123456789", pin: "123", honors: "22");
            IsFalse(new Honitsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "12345666778899");
            IsFalse(new Honitsu().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "123455667", honors: "11122");
            var winTile = TileId.Parse(honors: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, man: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChinitsuTest()
        {
            var tiles = Tiles34.Parse(man: "12345666778899");
            IsTrue(new Chinitsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "123456778899", honors: "22");
            IsFalse(new Chinitsu().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "11234567677889");
            var winTile = TileId.Parse(man: "1");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(6, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, man: "678") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsIttsuTest()
        {
            var tiles = Tiles34.Parse(man: "123456789", sou: "123", honors: "22");
            IsTrue(new Ittsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "112233456789", honors: "22");
            IsTrue(new Ittsu().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(man: "122334567789", honors: "11");
            IsFalse(new Ittsu().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "123456789", sou: "123", honors: "22");
            var winTile = TileId.Parse(sou: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            var melds = new List<Meld> { MakeMeld(MeldType.CHI, man: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHakuTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "555");
            IsTrue(new Haku().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "555");
            var winTile = TileId.Parse(honors: "5");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsHatsuTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "666");
            IsTrue(new Hatsu().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "666");
            var winTile = TileId.Parse(honors: "6");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChunTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "777");
            IsTrue(new Chun().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "777");
            var winTile = TileId.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsEastTest()
        {
            var (playerWind, roundWind) = (EAST, WEST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "111");
            IsTrue(new YakuhaiEast().IsConditionMet(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "111");
            var winTile = TileId.Parse(honors: "1");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            roundWind = EAST;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);
        }

        [TestMethod]
        public void IsSouthTest()
        {
            var (playerWind, roundWind) = (SOUTH, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "222");
            IsTrue(new YakuhaiSouth().IsConditionMet(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "222");
            var winTile = TileId.Parse(honors: "2");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            roundWind = SOUTH;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);
        }

        [TestMethod]
        public void IsWestTest()
        {
            var (playerWind, roundWind) = (WEST, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "333");
            IsTrue(new YakuhaiWest().IsConditionMet(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "333");
            var winTile = TileId.Parse(honors: "3");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            roundWind = WEST;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);
        }

        [TestMethod]
        public void IsNorthTest()
        {
            var (playerWind, roundWind) = (NORTH, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honors: "444");
            IsTrue(new YakuhaiNorth().IsConditionMet(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileIds.Parse(man: "23422", sou: "234567", honors: "444");
            var winTile = TileId.Parse(honors: "4");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            roundWind = NORTH;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);
        }

        [TestMethod]
        public void DoraInHandTest()
        {
            var tiles = TileIds.Parse(man: "456789", sou: "345678", honors: "55");
            var winTile = TileId.Parse(sou: "5");
            var doraIndicators = TileIds.Parse(sou: "5");
            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "678") };
            var result = EstimateHandValue(tiles, winTile, melds, doraIndicators);
            AreNotEqual(null, result.Error);

            tiles = TileIds.Parse(man: "123456", pin: "33", sou: "123456");
            winTile = TileId.Parse(man: "6");
            doraIndicators = TileIds.Parse(pin: "2");
            result = EstimateHandValue(tiles, winTile, doraIndicators: doraIndicators);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "22456678", pin: "123678");
            winTile = TileId.Parse(man: "2");
            doraIndicators = TileIds.Parse(man: "1", pin: "2");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "678", pin: "34577", sou: "123345");
            winTile = TileId.Parse(pin: "7");
            doraIndicators = TileIds.Parse(sou: "44");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "678", pin: "345", sou: "123345", honors: "66");
            winTile = TileId.Parse(pin: "5");
            doraIndicators = TileIds.Parse(honors: "55");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "123678", pin: "44", sou: "12346");
            winTile = TileId.Parse(pin: "4");
            tiles.Add(new TileId(FIVE_RED_SOU));
            doraIndicators = TileIds.Parse(man: "1", pin: "2");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yaku.Count);

            tiles = TileIds.Parse(man: "777", pin: "34577", sou: "123345");
            winTile = TileId.Parse(pin: "7");
            melds = new List<Meld> { MakeMeld(MeldType.KAN, man: "7777", isOpen: false) };
            doraIndicators = TileIds.Parse(man: "6");
            result = EstimateHandValue(
                tiles, winTile, melds, doraIndicators, new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yaku.Count);
        }

        [TestMethod]
        public void IsAgariAndClosedKanTest()
        {
            var tiles = TileIds.Parse(man: "45666777", pin: "111", honors: "222");
            var winTile = TileId.Parse(man: "4");
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.PON, pin: "111"),
                MakeMeld(MeldType.KAN, man: "6666", isOpen: false),
                MakeMeld(MeldType.PON, man: "777")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void KazoeSettingsTest()
        {
            var tiles = TileIds.Parse(man: "22244466677788");
            var winTile = TileId.Parse(man: "7");
            var melds = new List<Meld> { MakeMeld(MeldType.KAN, man: "2222", isOpen: false) };
            var doraIndicators = TileIds.Parse(man: "1111");
            var config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_LIMITED));
            var result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(32000, result.Cost.Main);

            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_SANBAIMAN));
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(24000, result.Cost.Main);

            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_NO_LIMIT));
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(64000, result.Cost.Main);
        }

        [TestMethod]
        public void OpenHandWithoutAdditionalFuTest()
        {
            var tiles = TileIds.Parse(man: "234567", pin: "22", sou: "234678");
            var winTile = TileId.Parse(sou: "6");
            var melds = new List<Meld> { MakeMeld(MeldType.CHI, sou: "234") };
            var config = new HandConfig(options: new OptionalRules(hasOpenTanyao: true, fuForOpenPinfu: false));
            var result = EstimateHandValue(tiles, winTile, melds, config: config);
            AreEqual(1, result.Han);
            AreEqual(20, result.Fu);
            AreEqual(700, result.Cost.Main);
        }

        [TestMethod]
        public void AkaDoraTest()
        {
            var tiles = TileIds.Parse(man: "12355599", pin: "456", sou: "345", hasAkaDora: false);
            var winTile = TileId.Parse(man: "9");
            var handConfig = new HandConfig(isTsumo: true, options: new OptionalRules(hasAkaDora: true));
            var handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(4, handCalculation.Han);

            tiles = TileIds.Parse(man: "12355599", pin: "456", sou: "345", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(1, handCalculation.Han);

            tiles = TileIds.Parse(man: "12355599", pin: "456", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(2, handCalculation.Han);

            tiles = TileIds.Parse(man: "12355599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(3, handCalculation.Han);

            tiles = TileIds.Parse(man: "123r5599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(4, handCalculation.Han);

            tiles = TileIds.Parse(man: "123rr599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(5, handCalculation.Han);

            tiles = TileIds.Parse(man: "123rrr99", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(6, handCalculation.Han);
        }
    }
}