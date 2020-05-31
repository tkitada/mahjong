using mjlib.Tiles;
using System.Linq;

namespace mjlib
{
    internal class Meld
    {
        public MeldType Type { get; }
        public Tiles136 Tiles { get; }
        public bool Opend { get; }
        public TileID CalledTile { get; }
        public int Who { get; }
        public int? FromWho { get; }

        public Tiles34 Tiles34 => 
            new Tiles34(Tiles.Take(3)
                             .Select(t => t.Value / 4)
                             .ToList());

        public Meld(MeldType meldType = MeldType.None, Tiles136 tiles = null, bool opend = true, TileID calledTile = null, int who = 0, int? fromWho = null)
        {
            Type = meldType;
            Tiles = tiles;
            Opend = opend;
            CalledTile = calledTile;
            Who = who;
            FromWho = fromWho;
        }
    }

    internal enum MeldType
    {
        None,
        CHI,
        PON,
        KAN,
        CHANKAN,
        NUKI,
    }
}