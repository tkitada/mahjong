using mjlib.Tiles;
using System.Linq;

namespace mjlib
{
    public class Meld
    {
        public MeldType Type { get; }
        public TileIds Tiles { get; }
        public bool Opened { get; }
        public TileId CalledTile { get; }
        public int? Who { get; }
        public int? FromWho { get; }

        public TileKinds TileKinds =>
            new TileKinds(Tiles.Take(3)
                             .Select(t => t.Value / 4));

        public Meld(MeldType meldType = MeldType.None,
            TileIds tiles = null,
            bool opened = true,
            TileId calledTile = null,
            int? who = null,
            int? fromWho = null)
        {
            Type = meldType;
            Tiles = tiles;
            Opened = opened;
            CalledTile = calledTile;
            Who = who;
            FromWho = fromWho;
        }

        public override string ToString()
        {
            return $"Type: {Type}\tTiles: {Tiles.ToOneLineString()}";
        }
    }

    public enum MeldType
    {
        None,
        CHI,
        PON,
        KAN,
        CHANKAN,
        NUKI,
    }
}