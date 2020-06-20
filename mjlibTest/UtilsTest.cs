using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
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

            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "1").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "2").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "3").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "4").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "5").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "6").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "7").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "8").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(man: "9").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "1").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "2").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "3").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "4").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "5").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "6").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "7").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "8").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(pin: "9").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "1").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "2").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "3").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "4").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "5").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "6").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "7").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "8").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(sou: "9").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "1").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "2").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "3").Value));
            IsFalse(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "4").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "5").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "6").Value));
            IsTrue(isoTiles.Select(t => t.Value).Contains(StringToTileKind(honors: "7").Value));
        }

        [TestMethod]
        public void IsStrictlyIsolatedTilesTest()
        {
            var hand = Tiles34.Parse(man: "25", pin: "15678", sou: "1399", honors: "1224");

            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "1")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "2")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "3")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "4")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "5")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "6")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(man: "7")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "8")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(man: "9")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "1")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "2")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "3")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "4")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "5")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "6")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "7")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "8")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(pin: "9")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "1")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "2")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "3")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "4")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "5")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "6")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "7")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "8")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(sou: "9")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "1")));
            IsFalse(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "2")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "3")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "4")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "5")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "6")));
            IsTrue(hand.IsTileStrictlyIsolated(StringToTileKind(honors: "7")));
        }
    }
}