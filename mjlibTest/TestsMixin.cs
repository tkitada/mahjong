using mjlib.Tiles;
using System.Linq;

namespace mjlibTest
{
    internal static class TestsMixin
    {
        public static TileKinds StringToOpenTiles34(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var openSet = TileIds.Parse(man: man, pin: pin, sou: sou, honors: honors);
            return new TileKinds(openSet.Select(t => t.Value / 4));
        }

        public static TileKind StringToTileKind(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var item = TileIds.Parse(man: man, pin: pin, sou: sou, honors: honors);
            return new TileKind(item[0].Value / 4);
        }
    }
}