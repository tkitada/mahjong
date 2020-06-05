using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.Tiles;
using System.Collections.Generic;
using static mjlib.Shanten;
using static mjlibTest.TestsMixin;

namespace mjlibTest
{
    [TestClass]
    public class ShantenTest
    {
        [TestMethod]
        public void ShantenNumberTest()
        {
            var tiles = TilesSet.Parse(man: "567", pin: "11", sou: "111234567");
            Assert.AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "567", pin: "11", sou: "111345677");
            Assert.AreEqual(0, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "567", pin: "15", sou: "111345677");
            Assert.AreEqual(1, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "1578", pin: "15", sou: "11134567");
            Assert.AreEqual(2, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "1358", pin: "1358", sou: "113456");
            Assert.AreEqual(3, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "1358", pin: "13588", sou: "1589", honors: "1");
            Assert.AreEqual(4, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "1358", pin: "13588", sou: "159", honors: "12");
            Assert.AreEqual(5, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "1358", pin: "258", sou: "1589", honors: "123");
            Assert.AreEqual(6, CalculateShanten(tiles));

            tiles = TilesSet.Parse(sou: "11123456788999");
            Assert.AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = TilesSet.Parse(sou: "11122245679999");
            Assert.AreEqual(0, CalculateShanten(tiles));
        }

        [TestMethod]
        public void ShantenNumberAndChitoitsuTest()
        {
            var tiles = TilesSet.Parse(man: "77", pin: "114477", sou: "114477");
            Assert.AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "76", pin: "114477", sou: "114477");
            Assert.AreEqual(0, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "76", pin: "114479", sou: "114477");
            Assert.AreEqual(1, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "76", pin: "14479", sou: "114477", honors: "1");
            Assert.AreEqual(2, CalculateShanten(tiles));
        }

        [TestMethod]
        public void ShantenNumberAndKokushimusou()
        {
            var tiles = TilesSet.Parse(man: "19", pin: "19", sou: "19", honors: "12345677");
            Assert.AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "19", sou: "129", honors: "1234567");
            Assert.AreEqual(0, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "129", sou: "129", honors: "123456");
            Assert.AreEqual(1, CalculateShanten(tiles));

            tiles = TilesSet.Parse(man: "129", pin: "129", sou: "129", honors: "12345");
            Assert.AreEqual(2, CalculateShanten(tiles));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var tiles = TilesSet.Parse(pin: "222567", sou: "44467778");
            Assert.AreEqual(AGARI_STATE, CalculateShanten(tiles));

            var melds = new List<Tiles34>
            {
                StringToOpenTiles34(sou:"777")
            };
            Assert.AreEqual(0, CalculateShanten(tiles, melds));

            tiles = TilesSet.Parse(man: "345", pin: "222", sou: "23455567");
            melds = new List<Tiles34>
            {
                StringToOpenTiles34(man:"345"),
                StringToOpenTiles34(sou:"555")
            };
            Assert.AreEqual(0, CalculateShanten(tiles, melds));
        }
    }
}