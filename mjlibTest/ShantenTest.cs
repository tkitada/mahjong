using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
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
            var tiles = Tiles34.Parse(man: "567", pin: "11", sou: "111234567");
            AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "567", pin: "11", sou: "111345677");
            AreEqual(0, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "567", pin: "15", sou: "111345677");
            AreEqual(1, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "1578", pin: "15", sou: "11134567");
            AreEqual(2, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "1358", pin: "1358", sou: "113456");
            AreEqual(3, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "1358", pin: "13588", sou: "1589", honors: "1");
            AreEqual(4, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "1358", pin: "13588", sou: "159", honors: "12");
            AreEqual(5, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "1358", pin: "258", sou: "1589", honors: "123");
            AreEqual(6, CalculateShanten(tiles));

            tiles = Tiles34.Parse(sou: "11123456788999");
            AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = Tiles34.Parse(sou: "11122245679999");
            AreEqual(0, CalculateShanten(tiles));
        }

        [TestMethod]
        public void ShantenNumberAndChitoitsuTest()
        {
            var tiles = Tiles34.Parse(man: "77", pin: "114477", sou: "114477");
            AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "76", pin: "114477", sou: "114477");
            AreEqual(0, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "76", pin: "114479", sou: "114477");
            AreEqual(1, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "76", pin: "14479", sou: "114477", honors: "1");
            AreEqual(2, CalculateShanten(tiles));
        }

        [TestMethod]
        public void ShantenNumberAndKokushimusou()
        {
            var tiles = Tiles34.Parse(man: "19", pin: "19", sou: "19", honors: "12345677");
            AreEqual(AGARI_STATE, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "19", sou: "129", honors: "1234567");
            AreEqual(0, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "129", sou: "129", honors: "123456");
            AreEqual(1, CalculateShanten(tiles));

            tiles = Tiles34.Parse(man: "129", pin: "129", sou: "129", honors: "12345");
            AreEqual(2, CalculateShanten(tiles));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var tiles = Tiles34.Parse(pin: "222567", sou: "44467778");
            AreEqual(AGARI_STATE, CalculateShanten(tiles));

            var melds = new List<TileKinds>
            {
                StringToOpenTiles34(sou:"777")
            };
            AreEqual(0, CalculateShanten(tiles, melds));

            tiles = Tiles34.Parse(man: "345", pin: "222", sou: "23455567");
            melds = new List<TileKinds>
            {
                StringToOpenTiles34(man:"345"),
                StringToOpenTiles34(sou:"555")
            };
            AreEqual(0, CalculateShanten(tiles, melds));
        }
    }
}