using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlibTest
{
    /// <summary>
    /// TileTest の概要の説明
    /// </summary>
    [TestClass]
    public class TilesTest
    {
        [TestMethod]
        public void Tiles136ToOneLineStringTest()
        {
            var tiles = new TileIds(new List<int>
            {
                0, 1, 34, 35, 36, 37, 70, 71, 72, 73, 106, 107, 108, 109, 133, 134
            });

            var expected = "1199m1199p1199s1177z";
            var actual = tiles.ToOneLineString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Tiles136ToOneLineStringWithAkaDoraTest()
        {
            var tiles = new TileIds(new List<int>
            {
                1, 16, 13, 46, 5, 13, 24, 34, 134, 124
            });

            var expected = "1244579m3p57z";
            var actual = tiles.ToOneLineString(printAkaDora: false);
            Assert.AreEqual(expected, actual);

            expected = "1244079m3p57z";
            actual = tiles.ToOneLineString(printAkaDora: true);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Tiles136ToTilesSet()
        {
            var tiles = new TileIds(new List<int>
            {
                0, 34, 35, 36, 37, 70, 71, 72, 73, 106, 107, 108, 109, 134
            });

            var actual = tiles.ToTilesSet();
            Assert.AreEqual(1, actual[0]);
            Assert.AreEqual(2, actual[8]);
            Assert.AreEqual(2, actual[9]);
            Assert.AreEqual(2, actual[17]);
            Assert.AreEqual(2, actual[18]);
            Assert.AreEqual(2, actual[26]);
            Assert.AreEqual(2, actual[27]);
            Assert.AreEqual(1, actual[33]);
            Assert.AreEqual(14, actual.Sum());
        }

        [TestMethod]
        public void TilesSetToTiles136Test()
        {
            var expected = new TileIds(new List<int>
            {
                0, 32, 33, 36, 37, 68, 69, 72, 73, 104, 105, 108, 109, 132
            });
            var t = expected.ToTilesSet();
            var actual = t.ToTile136();
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Value, actual[i].Value);
            }
        }

        [TestMethod]
        public void StringToTiles136()
        {
            var expected = new TileIds(new List<int>
            {
                0, 32, 36, 68, 72, 104, 108, 112, 116, 120, 124, 128, 132
            });
            var actual = TileIds.Parse(man: "19", pin: "19", sou: "19", honors: "1234567");

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Value, actual[i].Value);
            }
        }

        [TestMethod]
        public void FindTileKindInTiles136()
        {
            var actual = new TileIds(new List<int>
            {
                3,4,5,6
            }).FindTileKindInTiles136(new TileKind(0));
            Assert.AreEqual(3, actual.Value);

            actual = new TileIds(new List<int>
            {
                3, 4, 134, 135
            }).FindTileKindInTiles136(new TileKind(33));
            Assert.AreEqual(134, actual.Value);

            actual = new TileIds(new List<int>
            {
                3, 4, 134, 135
            }).FindTileKindInTiles136(new TileKind(20));
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void StringToTiles136WithAkaDora()
        {
            var tiles = TileIds.Parse(man: "2244", pin: "333r67", sou: "44", hasAkaDora: true);
            Assert.IsTrue(tiles.Select(t => t.Value).Contains(FIVE_RED_PIN));

            tiles = TileIds.Parse(man: "2244", pin: "333067", sou: "44", hasAkaDora: true);
            Assert.IsTrue(tiles.Select(t => t.Value).Contains(FIVE_RED_PIN));
        }

        [TestMethod]
        public void OneLineStringToTiles136()
        {
            var initialString = "789m456p555s11222z";
            var tiles = TileIds.Parse(str: initialString);
            Assert.AreEqual(14, tiles.Count);

            var newString = tiles.ToOneLineString();
            Assert.AreEqual(initialString, newString);
        }

        [TestMethod]
        public void OneLineStringToTilesSet()
        {
            var initialString = "789m456p555s11222z";
            var tiles = Tiles34.Parse(str: initialString);
            Assert.AreEqual(34, tiles.Count);

            var newString = tiles.ToOneLineString();
            Assert.AreEqual(initialString, newString);
        }
    }
}