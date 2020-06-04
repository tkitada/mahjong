using mjlib.Tiles;

namespace mjlibTest
{
    internal class TestsMixin
    {
        public static TileKind StringToTileKind(string man = "", string pin = "", string sou = "", string honors = "")
        {
            var item = Tiles136.Parse(man: man, pin: pin, sou: sou, honors: honors);
            return new TileKind(item[0].Value / 4);
        }
    }
}