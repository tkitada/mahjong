using mjlib.Tiles;
using System.Linq;

namespace mjlibTest
{
    internal static class TestsMixin
    {
        public static Tiles34 StringToOpenTiles34(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var openSet = Tiles136.Parse(man: man, pin: pin, sou: sou, honors: honors);
            return new Tiles34(openSet.Select(t => t.Value / 4).ToList());
        }

        public static TileKind StringToTileKind(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var item = Tiles136.Parse(man: man, pin: pin, sou: sou, honors: honors);
            return new TileKind(item[0].Value / 4);
        }
    }
}