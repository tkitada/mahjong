using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.Tiles;
using System.Collections.Generic;
using static mjlib.Agari;
using static mjlibTest.TestsMixin;

namespace mjlibTest
{
    [TestClass]
    public class AgariTest
    {
        [TestMethod]
        public void IsAgariTest()
        {
            var tiles = TilesSet.Parse(man: "33", pin: "123", sou: "123456789");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(pin: "11123", sou: "123456789");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(sou: "123456789", honors: "11777");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(sou: "12345556778899");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(sou: "11123456788999");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "345", pin: "789", sou: "233334", honors: "55");
            Assert.IsTrue(IsAgari(tiles));
        }

        [TestMethod]
        public void IsNotAgari()
        {
            var tiles = TilesSet.Parse(pin: "12345", sou: "123456789");
            Assert.IsFalse(IsAgari(tiles));

            tiles = TilesSet.Parse(pin: "11145", sou: "111222444");
            Assert.IsFalse(IsAgari(tiles));

            tiles = TilesSet.Parse(sou: "11122233356888");
            Assert.IsFalse(IsAgari(tiles));
        }

        [TestMethod]
        public void IsChitoitsuAgariTest()
        {
            var tiles = TilesSet.Parse(pin: "1199", sou: "1133557799");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "11", pin: "1199", sou: "2244", honors: "2277");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "11223344556677");
            Assert.IsTrue(IsAgari(tiles));
        }

        [TestMethod]
        public void IsKokushimusouAgariTest()
        {
            var tiles = TilesSet.Parse(man: "199", pin: "19", sou: "19", honors: "1234567");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "19", sou: "19", honors: "11234567");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "19", sou: "19", honors: "12345677");
            Assert.IsTrue(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "19", sou: "129", honors: "1234567");
            Assert.IsFalse(IsAgari(tiles));

            tiles = TilesSet.Parse(man: "19", pin: "19", sou: "19", honors: "11134567");
            Assert.IsFalse(IsAgari(tiles));
        }

        [TestMethod]
        public void IsAgariAndOpenHandTest()
        {
            var tiles = TilesSet.Parse(man: "345", pin: "222", sou: "23455567");
            var melds = new List<Tiles34>
            {
                StringToOpenTiles34(man:"345"),
                StringToOpenTiles34(sou:"555")
            };
            Assert.IsFalse(IsAgari(tiles, melds));
        }
    }
}