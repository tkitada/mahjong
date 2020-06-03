using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace mjlib
{
    internal class Agari
    {
        public static bool IsAgari(TilesSet tilesSet, List<Tiles34> openSets)
        {
            var tiles = new TilesSet(tilesSet.Select(t => t).ToList());

            if (openSets.Count != 0)
            {
                var isolatedTiles = tiles.FindIsolatedTileIndices();
                foreach (var meld in openSets)
                {
                    if (isolatedTiles.Count == 0) break;

                    var lastIndex = isolatedTiles.Count - 1;
                    var isolatedTile = isolatedTiles[lastIndex];
                    isolatedTiles.RemoveAt(lastIndex);

                    tiles[meld[0].Value] -= 1;
                    tiles[meld[1].Value] -= 1;
                    tiles[meld[2].Value] -= 1;
                    tiles[isolatedTile.Value] = 3;
                }
            }

            //jは4桁のバイナリ
            var j = (1 << tiles[27]) | (1 << tiles[28]) | (1 << tiles[29]) | (1 << tiles[30]) |
                (1 << tiles[31]) | (1 << tiles[32]) | (1 << tiles[33]);

            //jに5桁目はない
            if (j >= 0x10) return false;

            //国士無双判定
            //1桁目:0-すべての字牌は1つ以上存在する
            //2桁目:1-すべての字牌が2つ以上であってはならない
            //么九牌のうち2つの牌が1種類、それ以外は1つ
            if ((j & 3) == 2
                && tiles[0] * tiles[8] * tiles[9] * tiles[17] * tiles[18] * tiles[26] *
                   tiles[27] * tiles[28] * tiles[29] * tiles[30] * tiles[31] * tiles[32] * tiles[33] == 2)
                return true;

            //七対子判定
            //2,4桁目:0-すべての牌が0,2,4つのいずれか
            //2つの牌が7つ存在する
            if ((j & 10) == 0 && Range(0, 34).Count(i => tiles[i] == 2) == 7)
                return true;

            //2桁目:0-すべての字牌は2つ以上か0つである
            if ((j & 2) != 0) return false;

            //(1,4,7),(2,5,8),(3,6,9)それぞれの個数を集めたもの
            //萬子
            var n00 = tiles[0] + tiles[3] + tiles[6];
            var n01 = tiles[1] + tiles[4] + tiles[7];
            var n02 = tiles[2] + tiles[5] + tiles[8];
            //索子
            var n10 = tiles[9] + tiles[12] + tiles[15];
            var n11 = tiles[10] + tiles[13] + tiles[16];
            var n12 = tiles[11] + tiles[14] + tiles[17];
            //筒子
            var n20 = tiles[18] + tiles[21] + tiles[24];
            var n21 = tiles[19] + tiles[22] + tiles[25];
            var n22 = tiles[20] + tiles[23] + tiles[26];

            //対子があれば2, 面子のみなら0
            var n0 = (n00 + n01 + n02) % 3;
            var n1 = (n10 + n11 + n12) % 3;
            var n2 = (n20 + n21 + n22) % 3;
            if (n0 == 1 || n1 == 1 || n2 == 1) return false;

            //雀頭は1つだけ
            if (new List<int> { n0, n1, n2,
                tiles[27], tiles[28], tiles[29], tiles[30],
                tiles[31],tiles[32],tiles[33]}.Count(n => n == 2) != 1)
                return false;

            //面子を消す
            //(1,4,7)%3=1,(2,5,8)%3=2, (3,6,9)%3=0
            //[123]=>(1*1+2*1=3)=0, [444]=>(1*3)%3=0
            //0:(3,6,9)に,1;(2,5,8)に,2:(3,6,9)に雀頭がある
            //[77]=>(1*2)%3=2, [88]=>(2*2)%3=1,[99]=>(0*2)%3=0
            var nn0 = (n00 * 1 + n01 * 2) % 3;
            var nn1 = (n10 * 1 + n11 * 2) % 3;
            var nn2 = (n20 * 1 + n21 * 2) % 3;

            var m0 = ToMeld(tiles, 0);
            var m1 = ToMeld(tiles, 9);
            var m2 = ToMeld(tiles, 18);

            //3桁目:1-字牌に対子がある
            if ((j & 4) != 0)
            {
                //字牌以外面子しかもたない 以下も同様
                return (n0 | nn0 | n1 | nn1 | n2 | nn2) == 0
                    && IsMentsu(m0)
                    && IsMentsu(m1)
                    && IsMentsu(m2);
            }
            //萬子に対子がある
            if (n0 == 2)
            {
                return (n1 | nn1 | n2 | nn2) == 0
                    && IsMentsu(m1)
                    && IsMentsu(m2)
                    && IsAtamaMentsu(nn0, m0);
            }
            //筒子に対子がある
            if (n1 == 2)
            {
                return (n2 | nn2 | n0 | nn0) == 0
                    && IsMentsu(m2)
                    && IsMentsu(m0)
                    && IsAtamaMentsu(nn1, m1);
            }
            //索子に対子がある
            if (n2 == 2)
            {
                return (n0 | nn0 | n1 | nn1) == 0
                    && IsMentsu(m0)
                    && IsMentsu(m1)
                    && IsAtamaMentsu(nn2, m2);
            }
            return false;
        }

        //Tilesの各intをバイナリにしたものが右から3桁ずつ並ぶ
        //[0,1,2,3,4]=>0b100_011_010_001_000
        private static int ToMeld(TilesSet tiles, int d)
        {
            var result = 0;
            for (var i = 0; i < 9; i++)
            {
                result |= tiles[d + i] << i * 3;
            }
            return result;
        }

        /// <summary>
        /// すべて面子で構成されているか否か
        /// </summary>
        /// <param name="m">ある色についてmeldで表したもの</param>
        /// <returns></returns>
        private static bool IsMentsu(int m)
        {
            //Tilesの左端の個数
            var a = m & 7;
            var b = 0;
            var c = 0;
            //1か4個のとき次とその次の1個と順子を構成するはず
            if (a == 1 || a == 4)
            {
                b = c = 1;
            }
            //2個のとき次とその次の2個と順子を構成するはず
            else if (a == 2)
            {
                b = c = 2;
            }
            m >>= 3;
            //想定される個数分削除する
            a = (m & 7) - b;
            //想定された個数を満たしていない
            if (a < 0) return false;

            for (var x = 0; x < 6; x++)
            {
                b = c;
                c = 0;
                if (a == 1 || a == 4)
                {
                    b++;
                    c++;
                }
                else if (a == 2)
                {
                    b += 2;
                    c += 2;
                }
                m >>= 3;
                a = (m & 7) - b;
                if (a < 0) return false;
            }
            m >>= 3;
            a = (m & 7) - c;
            return a == 0 || a == 3;
        }

        /// <summary>
        /// 雀頭が1つとそれ以外が面子で構成されているか否か
        /// </summary>
        /// <param name="nn">ある色について面子を消したもの</param>
        /// <param name="m">ある色についてmeldで表したもの</param>
        /// <returns></returns>
        private static bool IsAtamaMentsu(int nn, int m)
        {
            //(3,6,9)%3=0*2=>0
            //(3,6,9)に雀頭があり、それ以外が面子か否か
            if (nn == 0)
            {
                if ((m & (7 << 6)) >= (2 << 6) && IsMentsu(m - (2 << 6)))
                    return true;
                if ((m & (7 << 15)) >= (2 << 15) && IsMentsu(m - (2 << 15)))
                    return true;
                if ((m & (7 << 24)) >= (2 << 24) && IsMentsu(m - (2 << 24)))
                    return true;
            }
            //(2,5,8)%3=2*2=>1
            //(2,5,8)に雀頭があり、それ以外が面子か否か
            else if (nn == 1)
            {
                if ((m & (7 << 3)) >= (2 << 3) && IsMentsu(m - (2 << 3)))
                    return true;
                if ((m & (7 << 12)) >= (2 << 12) && IsMentsu(m - (2 << 12)))
                    return true;
                if ((m & (7 << 21)) >= (2 << 21) && IsMentsu(m - (2 << 21)))
                    return true;
            }
            //(1,4,7)%3=1*2=>2
            //(1,4,7)に雀頭があり、それ以外が面子か否か
            else if (nn == 2)
            {
                if ((m & (7 << 0)) >= (2 << 0) && IsMentsu(m - (2 << 0)))
                    return true;
                if ((m & (7 << 9)) >= (2 << 9) && IsMentsu(m - (2 << 9)))
                    return true;
                if ((m & (7 << 18)) >= (2 << 18) && IsMentsu(m - (2 << 18)))
                    return true;
            }
            return false;
        }
    }
}