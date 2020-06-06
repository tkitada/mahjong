using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.Tiles;
using System.Linq;
using static mjlibTest.TestsMixin;

namespace mjlibTest
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void FindIsolatedTilesTest()
        {
            var hand = Tiles34.Parse(man: "25", pin: "15678", sou: "1369", honors: "124");
            var isoTiles = hand.FindIsolatedTileIndices();

            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "1").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "2").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "3").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "4").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "5").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "6").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "7").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "8").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "9").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "1").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "2").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "3").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "4").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "5").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "6").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "7").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "8").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "9").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "1").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "2").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "3").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "4").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "5").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "6").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "7").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "8").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "9").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "1").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "2").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "3").Value));
            Assert.IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "4").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "5").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "6").Value));
            Assert.IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "7").Value));
        }

        [TestMethod]
        public void IsStrictlyIsolatedTilesTest()
        {
            var hand = Tiles34.Parse(man: "25", pin: "15678", sou: "1399", honors: "1224");

            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "1")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "2")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "3")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "4")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "5")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "6")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "7")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "8")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "9")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "1")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "2")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "3")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "4")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "5")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "6")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "7")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "8")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "9")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "1")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "2")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "3")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "4")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "5")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "6")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "7")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "8")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "9")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "1")));
            Assert.IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "2")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "3")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "4")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "5")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "6")));
            Assert.IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "7")));
        }
    }
}