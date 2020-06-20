using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
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
            var tiles = Tiles34.Parse(man: "33", pin: "123", sou: "123456789");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(pin: "11123", sou: "123456789");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(sou: "123456789", honors: "11777");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(sou: "12345556778899");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(sou: "11123456788999");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "345", pin: "789", sou: "233334", honors: "55");
            IsTrue(IsAgari(tiles));
        }

        [TestMethod]
        public void IsNotAgari()
        {
            var tiles = Tiles34.Parse(pin: "12345", sou: "123456789");
            IsFalse(IsAgari(tiles));

            tiles = Tiles34.Parse(pin: "11145", sou: "111222444");
            IsFalse(IsAgari(tiles));

            tiles = Tiles34.Parse(sou: "11122233356888");
            IsFalse(IsAgari(tiles));
        }

        [TestMethod]
        public void IsChitoitsuAgariTest()
        {
            var tiles = Tiles34.Parse(pin: "1199", sou: "1133557799");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "11", pin: "1199", sou: "2244", honors: "2277");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "11223344556677");
            IsTrue(IsAgari(tiles));
        }

        [TestMethod]
        public void IsKokushimusouAgariTest()
        {
            var tiles = Tiles34.Parse(man: "199", pin: "19", sou: "19", honors: "1234567");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "19", sou: "19", honors: "11234567");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "19", sou: "19", honors: "12345677");
            IsTrue(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "19", sou: "129", honors: "1234567");
            IsFalse(IsAgari(tiles));

            tiles = Tiles34.Parse(man: "19", pin: "19", sou: "19", honors: "11134567");
            IsFalse(IsAgari(tiles));
        }

        [TestMethod]
        public void IsAgariAndOpenHandTest()
        {
            var tiles = Tiles34.Parse(man: "345", pin: "222", sou: "23455567");
            var melds = new List<TileKinds>
            {
                StringToOpenTiles34(man:"345"),
                StringToOpenTiles34(sou:"555")
            };
            IsFalse(IsAgari(tiles, melds));
        }
    }
}