using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

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

        }

        private TileKinds GetWinGroup(IList<TileKinds> hand, TileId winTile)
        {
            return hand.Where(x => x.Contains(winTile.ToTileKind())).ToList()[0];
        }
    }
}