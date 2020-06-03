using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    public class Tiles136 : IEnumerable<TileID>
    {
        private readonly IList<TileID> tiles_;

        public int Count => tiles_.Count;

        public TileID this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public Tiles136()
        {
            tiles_ = new List<TileID>();
        }

        public Tiles136(IList<TileID> tiles)
        {
            tiles_ = tiles;
        }

        public Tiles136(IList<int> tiles)
        {
            tiles_ = tiles.Select(t => new TileID(t)).ToList();
        }

        public void Add(TileID item)
        {
            tiles_.Add(item);
        }

        public bool Contains(TileID item)
        {
            return tiles_.Contains(item);
        }

        public IEnumerator<TileID> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }

        public TilesSet ToTilesSet()
        {
            var result = new TilesSet();
            foreach (var tile in this)
            {
                result[tile.Value / 4] += 1;
            }
            return result;
        }

        public string ToOneLineString(bool printAkaDora = false)
        {
            var tiles = this.OrderBy(t => t.Value)
                            .Select(t => t.Value);

            var man = tiles.Where(t => t < 36);
            var pin = tiles.Where(t => 36 <= t && t < 72)
                           .Select(t => t - 36);
            var sou = tiles.Where(t => 72 <= t && t < 108)
                           .Select(t => t - 72);
            var honors = tiles.Where(t => t >= 108)
                              .Select(t => t - 108);

            string Words(IEnumerable<int> suits, int redFive, string suffix) =>
                suits.Count() == 0
                    ? ""
                    : string.Join("",
                        suits.Select(t => t == redFive && printAkaDora
                            ? "0"
                            : (t / 4 + 1).ToString()
                        )
                    ) + suffix;
            var manStr = Words(man, FIVE_RED_MAN, "m");
            var pinStr = Words(pin, FIVE_RED_PIN, "p");
            var souStr = Words(sou, FIVE_RED_SOU, "s");
            var honorsStr = Words(honors, -1, "z");

            return manStr + pinStr + souStr + honorsStr;
        }

        public static Tiles136 Parse(string man = "", string pin = "", string sou = "",
            string honors = "", bool hasAkaDora = false)
        {
            IList<int> SplitString(string str, int offset, int red)
            {
                var temp = new List<int>();
                var data = new List<int>();
                if (string.IsNullOrEmpty(str))
                {
                    return temp;
                }
                foreach (var i in str)
                {
                    if ((i == 'r' || i == '0') && hasAkaDora)
                    {
                        temp.Add(red);
                        data.Add(red);
                    }
                    else
                    {
                        var tile = offset + (int.Parse(i.ToString()) - 1) * 4;
                        if (tile == red && hasAkaDora)
                        {
                            tile += 1;
                        }
                        if (data.Contains(tile))
                        {
                            var count = temp.Count(x => x == tile);
                            var newTile = tile + count;
                            data.Add(newTile);
                            temp.Add(tile);
                        }
                        else
                        {
                            data.Add(tile);
                            temp.Add(tile);
                        }
                    }
                }
                return data;
            }
            var result = SplitString(man, 0, FIVE_RED_MAN).ToList();
            result.AddRange(SplitString(pin, 36, FIVE_RED_PIN));
            result.AddRange(SplitString(sou, 72, FIVE_RED_SOU));
            result.AddRange(SplitString(honors, 108, -1));

            return new Tiles136(result);
        }
    }
}