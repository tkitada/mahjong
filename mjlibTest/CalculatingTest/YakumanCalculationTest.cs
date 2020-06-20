using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.HandCalculating;
using mjlib.HandCalculating.YakuList.Yakuman;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.HandCalculating.HandCalculator;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class YakumanCalculationTest
    {
        [TestMethod]
        public void IsTenhouTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTenhou: true));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChiihouTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isChiihou: true));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsDaisangenTest()
        {
            var tiles = Tiles34.Parse(man: "22", sou: "123", honors: "555666777");
            IsTrue(new Daisangen().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "22", sou: "123", honors: "555666777");
            var winTile = TileId.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsShosuushiTest()
        {
            var tiles = Tiles34.Parse(sou: "123", honors: "11122233344");
            IsTrue(new Shousuushii().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(sou: "123", honors: "11122233344");
            var winTile = TileId.Parse(honors: "4");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsDaisuushiTest()
        {
            var tiles = Tiles34.Parse(sou: "22", honors: "111222333444");
            IsTrue(new DaiSuushi().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(sou: "22", honors: "111222333444");
            var winTile = TileId.Parse(honors: "4");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsTsuisouTest()
        {
            var tiles = Tiles34.Parse(honors: "11122233366677");
            IsTrue(new Tsuuiisou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(honors: "11223344556677");
            IsTrue(new Tsuuiisou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(honors: "1133445577", pin: "88", sou: "11");
            IsFalse(new Tsuuiisou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(honors: "11223344556677");
            var winTile = TileId.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(25, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChinrotoTest()
        {
            var tiles = Tiles34.Parse(man: "111999", pin: "99", sou: "111999");
            IsTrue(new Chinroutou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "111999", pin: "99", sou: "111222");
            var winTile = TileId.Parse(pin: "9");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsKokushiTest()
        {
            var tiles = Tiles34.Parse(man: "19", pin: "19", sou: "119", honors: "1234567");
            IsTrue(new KokushiMusou().IsConditionMet(null, new object[] { tiles }));

            var tiles_ = TileIds.Parse(man: "19", pin: "19", sou: "119", honors: "1234567");
            var winTile = TileId.Parse(sou: "9");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(0, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "19", pin: "19", sou: "119", honors: "1234567");
            winTile = TileId.Parse(sou: "1");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(0, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsRyuisouTest()
        {
            var tiles = Tiles34.Parse(sou: "22334466888", honors: "666");
            IsTrue(new Ryuuiisou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(sou: "22334466888", honors: "666");
            var winTile = TileId.Parse(honors: "6");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsSuuankouTest()
        {
            var tiles = Tiles34.Parse(man: "333", sou: "111444", pin: "44555");
            var winTile = TileId.Parse(sou: "4");
            IsTrue(new Suuankou().IsConditionMet(Hand(tiles), new object[] { winTile, true }));
            IsFalse(new Suuankou().IsConditionMet(Hand(tiles), new object[] { winTile, false }));

            var tiles_ = TileIds.Parse(man: "333", pin: "44555", sou: "111444");
            winTile = TileId.Parse(pin: "5");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);

            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false));
            AreNotEqual(13, result.Han);

            tiles_ = TileIds.Parse(man: "333", pin: "44455", sou: "111444");
            winTile = TileId.Parse(pin: "5");
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "33344455577799");
            winTile = TileId.Parse(man: "9");
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false));
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsChuurenpoutouTest()
        {
            var tiles = Tiles34.Parse(man: "11112345678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(pin: "11122345678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123345678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123445678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123455678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123456678999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123456778999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123456788999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            tiles = Tiles34.Parse(sou: "11123456789999");
            IsTrue(new ChuurenPoutou().IsConditionMet(Hand(tiles)));

            var tiles_ = TileIds.Parse(man: "11123456789999");
            var winTile = TileId.Parse(man: "1");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "11122345678999");
            winTile = TileId.Parse(man: "2");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "11123456789999");
            winTile = TileId.Parse(man: "9");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(man: "11112345678999");
            winTile = TileId.Parse(man: "1");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(26, result.Han);
            AreEqual(1, result.Yaku.Count);

            tiles_ = TileIds.Parse(pin: "11123456678999");
            winTile = TileId.Parse(pin: "3");
            var melds = new List<Meld> { MakeMeld(MeldType.KAN, pin: "9999", isOpen: false) };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(6, result.Han);
            AreEqual(70, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsSuukantsuTest()
        {
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, sou: "1111"),
                MakeMeld(MeldType.KAN, sou: "3333"),
                MakeMeld(MeldType.KAN, pin: "5555"),
                MakeMeld(MeldType.CHANKAN, man: "2222")
            };
            IsTrue(new Suukantsu().IsConditionMet(null, new object[] { melds }));

            var tiles = TileIds.Parse(man: "222", pin: "44555", sou: "111333");
            var winTile = TileId.Parse(pin: "4");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.KAN, sou: "1111"),
                MakeMeld(MeldType.KAN, sou: "3333"),
                MakeMeld(MeldType.KAN, pin: "5555"),
                MakeMeld(MeldType.KAN, man: "2222")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(70, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void DisabledDoubleYakumanTest()
        {
            var tiles = TileIds.Parse(man: "19", pin: "19", sou: "119", honors: "1234567");
            var winTile = TileId.Parse(sou: "1");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(options: new OptionalRules(hasDoubleYakuman: false)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(0, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(man: "333", pin: "44455", sou: "111444");
            winTile = TileId.Parse(pin: "5");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true, options: new OptionalRules(hasDoubleYakuman: false)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(man: "11122345678999");
            winTile = TileId.Parse(man: "2");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(options: new OptionalRules(hasDoubleYakuman: false)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(1, result.Yaku.Count);

            tiles = TileIds.Parse(sou: "22", honors: "111222333444");
            winTile = TileId.Parse(honors: "4");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(options: new OptionalRules(hasDoubleYakuman: false)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void SixtupleYakumanTest()
        {
            var tiles = TileIds.Parse(honors: "11122233344455");
            var winTile = TileId.Parse(honors: "5");
            var config = new HandConfig(isTenhou: true);
            var result = EstimateHandValue(tiles, winTile, config: config);
            AreEqual(null, result.Error);
            AreEqual(78, result.Han);
            AreEqual(192000, result.Cost.Main);
        }

        [TestMethod]
        public void KokushimusouMultipleYakuman()
        {
            var tiles = TileIds.Parse(man: "19", pin: "19", sou: "19", honors: "12345677");
            var winTile = TileId.Parse(honors: "1");
            var handConfig = new HandConfig(isTsumo: true, isTenhou: true, isChiihou: false);
            var handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(2, handCalculation.Yaku.Count);
            IsTrue(handCalculation.Yaku.Any(x => x.GetType() == typeof(KokushiMusou)));
            IsFalse(handCalculation.Yaku.Any(x => x.GetType() == typeof(DaburuKokushiMusou)));
            IsTrue(handCalculation.Yaku.Any(x => x.GetType() == typeof(Tenhou)));
            IsFalse(handCalculation.Yaku.Any(x => x.GetType() == typeof(Chiihou)));
            AreEqual(26, handCalculation.Han);
        }

        [TestMethod]
        public void IsRenhouYakumanTest()
        {
            var tiles = TileIds.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = TileId.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRenhou: true, options: new OptionalRules(renhouAsYakuman: true)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(1, result.Yaku.Count);
        }

        [TestMethod]
        public void IsNotDaisharinTest()
        {
            var tiles = TileIds.Parse(pin: "22334455667788");
            var winTile = TileId.Parse(pin: "8");
            var result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(11, result.Han);
            AreEqual(4, result.Yaku.Count);
        }

        [TestMethod]
        public void IsDaisharinTest()
        {
            var tiles = TileIds.Parse(pin: "22334455667788");
            var winTile = TileId.Parse(pin: "8");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(options: new OptionalRules(hasDaisharin: true)));
            AreEqual(null, result.Error);
            AreEqual(13, result.Han);
            AreEqual(1, result.Yaku.Count);
            AreEqual("Daisharin", result.Yaku[0].Name);
        }
    }
}