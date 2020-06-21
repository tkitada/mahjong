using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.Tiles
{
    /// <summary>
    /// 牌の種類id0 ~33をインデックスとして, それぞれの牌の個数を値として持つ
    /// </summary>
    public class Tiles34 : IEnumerable<int>, IEquatable<Tiles34>
    {
        private readonly List<int> tiles_;

        public int Count => tiles_.Count;

        public int this[int index]
        {
            get => tiles_[index];
            set => tiles_[index] = value;
        }

        public Tiles34()
        {
            tiles_ = new int[34].ToList();
        }

        public Tiles34(IEnumerable<int> tiles)
        {
            if (tiles.Count() != 34)
            {
                throw new ArgumentException();
            }
            tiles_ = tiles.ToList();
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

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is Tiles34 other
                && Equals(other);
        }

        public bool Equals(Tiles34 other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;
            for (var i = 0; i < tiles_.Count; i++)
            {
                if (!this[i].Equals(other[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public TileIds ToTileIds()
        {
            var temp = new List<int>();
            for (var x = 0; x < 34; x++)
            {
                for (var i = 0; i < this[x]; i++)
                {
                    temp.Add(x * 4 + i);
                }
            }
            return new TileIds(temp);
        }

        public static Tiles34 Parse(string str, bool hasAkaDora = false)
        {
            return TileIds.Parse(str: str, hasAkaDora: hasAkaDora).ToTiles34();
        }

        public static Tiles34 Parse(string man = "", string pin = "",
            string sou = "", string honors = "")
        {
            return TileIds.Parse(man: man, pin: pin, sou: sou, honors: honors).ToTiles34();
        }

        public string ToOneLineString()
        {
            return ToTileIds().ToOneLineString();
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
            var hand = new Tiles34(this);
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
            switch (simplified)
            {
                case 0:
                    indices = new List<int>
                    {
                        tile.Value,tile.Value+1,tile.Value+2
                    };
                    break;

                case 1:
                    indices = new List<int>
                    {
                       tile.Value-1, tile.Value,tile.Value+1,tile.Value+2
                    };
                    break;

                case 7:
                    indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value,tile.Value+1
                    };
                    break;

                case 8:
                    indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value
                    };
                    break;

                default:
                    indices = new List<int>
                    {
                       tile.Value-2, tile.Value-1, tile.Value,tile.Value+1,tile.Value+2
                    };
                    break;
            }
            return indices.All(x => hand[x] == 0);
        }
    }
}