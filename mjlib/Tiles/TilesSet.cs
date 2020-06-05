using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    public class TilesSet : IEnumerable<int>
    {
        private readonly IList<int> tiles_;

        public int Count => tiles_.Count;

        public int this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public TilesSet()
        {
            tiles_ = new int[34].ToList();
        }

        public TilesSet(IList<int> tiles)
        {
            if (tiles.Count != 34)
            {
                throw new ArgumentException();
            }
            tiles_ = tiles;
        }

        public bool Contains(int item)
        {
            return tiles_.Contains(item);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return tiles_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tiles_).GetEnumerator();
        }

        public Tiles136 ToTile136()
        {
            var temp = new List<int>();
            for (var x = 0; x < 34; x++)
            {
                for (var i = 0; i < this[x]; i++)
                {
                    temp.Add(x * 4 + i);
                }
            }
            return new Tiles136(temp);
        }

        public static TilesSet Parse(string str, bool hasAkaDora = false)
        {
            return Tiles136.Parse(str: str, hasAkaDora: hasAkaDora).ToTilesSet();
        }

        public static TilesSet Parse(string man = "", string pin = "",
            string sou = "", string honors = "")
        {
            return Tiles136.Parse(man: man, pin: pin, sou: sou, honors: honors).ToTilesSet();
        }

        public string ToOneLineString()
        {
            return ToTile136().ToOneLineString();
        }

        public bool ContainsTerminals()
        {
            return this.Any(x => TERMINAL_INDICES.Contains(x));
        }

        /// <summary>
        /// 自身及び隣り合った牌が存在しないTileKindのリストを返す
        /// </summary>
        /// <returns></returns>
        internal IList<TileKind> FindIsolatedTileIndices()
        {
            var isolatedIndices = new List<TileKind>();

            for (var x = 0; x <= CHUN; x++)
            {
                if (new TileKind(x).IsHonor && this[x] == 0)
                {
                    isolatedIndices.Add(new TileKind(x));
                    continue;
                }
                var simplified = new TileKind(x).Simplify;

                if (simplified == 0)
                {
                    if (this[x] == 0 && this[x + 1] == 0)
                    {
                        isolatedIndices.Add(new TileKind(x));
                    }
                }
                else if (simplified == 8)
                {
                    if (this[x - 1] == 0 && this[x] == 0)
                    {
                        isolatedIndices.Add(new TileKind(x));
                    }
                }
                else
                {
                    if (this[x - 1] == 0 && this[x] == 0 && this[x + 1] == 0)
                    {
                        isolatedIndices.Add(new TileKind(x));
                    }
                }
            }
            return isolatedIndices;
        }

        internal bool IsTileStrictlyIsolated(TileKind tile)
        {
            var hand = new TilesSet(this.ToList());
            hand[tile.Value] -= 1;
            if (hand[tile.Value] < 0)
            {
                hand[tile.Value] = 0;
            }
            var indices = new List<int>();
            if (tile.IsHonor)
            {
                return hand[tile.Value] == 0;
            }
            var simplified = tile.Simplify;
            if (simplified == 0)
            {
                indices = new List<int>
                    {
                        tile.Value,tile.Value+1,tile.Value+2
                    };
            }
            else if (simplified == 1)
            {
                indices = new List<int>
                    {
                       tile.Value-1, tile.Value,tile.Value+1,tile.Value+2
                    };
            }
            else if (simplified == 7)
            {
                indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value,tile.Value+1
                    };
            }
            else if (simplified == 8)
            {
                indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value
                    };
            }
            else
            {
                indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value,tile.Value+1,tile.Value+2
                    };
            }
            return indices.All(x => hand[x] == 0);
        }
    }
}