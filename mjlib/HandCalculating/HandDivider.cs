using mjlib.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static mjlib.Constants;

namespace mjlib.HandCalculating
{
    internal static class HandDivider
    {
        public static IList<List<TileKinds>> DivideHand(Tiles34 tiles34, IList<Meld> melds = null)
        {
            if (melds is null)
            {
                melds = new List<Meld>();
            }

            var closedHandTiles34 = new Tiles34(tiles34.Select(x => x));
            var openTileIndices = melds.Count != 0
                ? melds.Select(x => x.TileKinds)
                       .Aggregate((x, y) => new TileKinds(Enumerable.Concat(x, y)))
                : new TileKinds();
            foreach (var openItem in openTileIndices)
            {
                closedHandTiles34[openItem.Value] -= 1;
            }
            var pairIndices = FindPairs(closedHandTiles34);

            var hands = new List<List<TileKinds>>();

            foreach (var pairIndex in pairIndices)
            {
                var localTiles34 = new Tiles34(tiles34.Select(x => x));

                //すでに鳴いている牌は形が決まっているので外す
                foreach (var openItem in openTileIndices)
                {
                    localTiles34[openItem.Value] -= 1;
                }

                //雀頭候補を外す
                localTiles34[pairIndex.Value] -= 2;

                var man = FindValidCombinations(localTiles34, 0, 8);
                var pin = FindValidCombinations(localTiles34, 9, 17);
                var sou = FindValidCombinations(localTiles34, 18, 26);

                var honor = new List<IEnumerable<int>>();
                var honors = new List<IEnumerable<TileKinds>>();
                foreach (var x in HONOR_INDICES)
                {
                    if (localTiles34[x] == 3)
                    {
                        honor.Add(Enumerable.Repeat(x, 3));
                    }
                }
                if (honor.Count != 0)
                {
                    honors = new List<IEnumerable<TileKinds>>
                    {
                        honor.Select(x => new TileKinds(x))
                    };
                }

                var arrays = new List<IEnumerable<IEnumerable<TileKinds>>>
                {
                    new List<IEnumerable<TileKinds>>
                    {
                        new List<TileKinds>
                        {
                            new TileKinds(Enumerable.Repeat(pairIndex.Value,2))
                        }
                    }
                };
                if (man.Count() != 0)
                {
                    arrays.Add(man);
                }
                if (pin.Count() != 0)
                {
                    arrays.Add(pin);
                }
                if (sou.Count() != 0)
                {
                    arrays.Add(sou);
                }
                if (honors.Count() != 0)
                {
                    arrays.Add(honors);
                }
                foreach (var meld in melds)
                {
                    arrays.Add(new List<IEnumerable<TileKinds>>
                    {
                        new List<TileKinds>
                        {
                            meld.TileKinds
                        }
                    });
                }

                foreach (var s in Product(arrays))
                {
                    var hand = new List<TileKinds>();
                    foreach (var item in s)
                    {
                        foreach (var x in item)
                        {
                            hand.Add(x);
                        }
                    }
                    if (hand.Count == 5)
                    {
                        hand.Sort((x, y) => x[0].CompareTo(y[0]));
                        hands.Add(hand);
                    }
                }
            }
            var unique_hands = new List<List<TileKinds>>();
            foreach (var hand in hands)
            {
                hand.Sort((x, y) =>
                {
                    if (x[0].CompareTo(y[0]) > 0) return 1;
                    if (x[0].CompareTo(y[0]) < 0) return -1;
                    return x[1].CompareTo(y[1]);
                });
                if (unique_hands.All(x =>
                {
                    var lx = x.ToList();
                    var lhands = hand.ToList();
                    if (lx.Count != lhands.Count) return false;
                    for (var i = 0; i < lx.Count; i++)
                    {
                        if (lx[0].Equals(lhands[0])) return false;
                    }
                    return true;
                }))
                {
                    unique_hands.Add(hand);
                }
            }
            hands = unique_hands;

            if (pairIndices.Count == 7)
            {
                var hand = new List<TileKinds>();
                foreach (var index in pairIndices)
                {
                    hand.Add(new TileKinds(Enumerable.Repeat(index.Value, 2)));
                }
                hands.Add(hand);
            }
            hands.Sort((x, y) =>
            {
                var min = Math.Min(x.Count, y.Count);
                for (var i = 0; i < min; i++)
                {
                    if (x[i].CompareTo(y[i]) > 0) return 1;
                    if (x[i].CompareTo(y[i]) < 0) return -1;
                }
                return x.Count > y.Count ? 1 : x.Count < y.Count ? -1 : 0;
            });
            return hands;
        }

        private static TileKinds FindPairs(Tiles34 tiles34,
            int firstIndex = 0,
            int secondIndex = 33)
        {
            var pairIndices = new TileKinds();
            for (var x = firstIndex; x <= secondIndex; x++)
            {
                //字牌の刻子は無視する（途中で分断して対子にはできない）
                if (HONOR_INDICES.Contains(x) && tiles34[x] != 2) continue;

                if (tiles34[x] >= 2)
                {
                    pairIndices.Add(new TileKind(x));
                }
            }
            return pairIndices;
        }

        private static IEnumerable<IEnumerable<TileKinds>> FindValidCombinations(Tiles34 tiles34,
            int firstIndex,
            int secondIndex,
            bool handNotCompleted = false)
        {
            //Tiles34[0,1,1,1,2,...]=>TileKinds[1,2,3,4,4,...]
            var indices = new TileKinds();
            for (var x = firstIndex; x <= secondIndex; x++)
            {
                if (tiles34[x] > 0)
                {
                    var l = indices.ToList();
                    l.AddRange(Enumerable.Repeat(new TileKind(x), tiles34[x]));
                    indices = new TileKinds(l);
                }
            }
            if (indices.Count == 0)
                return new List<IEnumerable<TileKinds>>();

            //TileKindsのnP3全順列を列挙
            //[1,2,3,4,4]=>[[1,2,3],[1,2,4],[1,3,2],[1,3,4],[1,4,2],[1,4,3],[1,4,4],...]
            var t = indices.Permutations(3);
            var allPossibleCombinations = t.Select(x => new TileKinds(x));

            //刻子、順子の形をしている順列を抜きだす
            var validCombinations = new List<TileKinds>();
            foreach (var combination in allPossibleCombinations)
            {
                if (combination.IsChi || combination.IsPon)
                {
                    validCombinations.Add(combination);
                }
            }
            if (validCombinations.Count == 0)
                return new List<IEnumerable<TileKinds>>();

            var countOfNeededCombinations = indices.Count / 3;

            //有り得る順列のセットが一通りしかないとき
            if (countOfNeededCombinations == validCombinations.Count
                && indices.Equals(validCombinations.Aggregate(
                        (x, y) => new TileKinds(Enumerable.Concat(x, y)))))
                return new List<IEnumerable<TileKinds>> { validCombinations };

            var validCombinationsCopy = new List<TileKinds>(validCombinations);
            foreach (var item in validCombinations)
            {
                if (!item.IsPon) continue;
                var countOfSets = 1;
                var countOfTiles = 0;
                while (countOfSets > countOfTiles)
                {
                    countOfTiles = indices.Where(x => item[0].Equals(x))
                                          .Count() / 3;
                    countOfSets = validCombinationsCopy.Where(x => item[0].Equals(x[0])
                                                                && item[1].Equals(x[1])
                                                                && item[2].Equals(x[2]))
                                                       .Count();
                    if (countOfSets > countOfTiles)
                    {
                        validCombinationsCopy.Remove(item);
                    }
                }
            }
            validCombinations = validCombinationsCopy;
            validCombinationsCopy = new List<TileKinds>(validCombinations);
            foreach (var item in validCombinations)
            {
                if (!item.IsChi) continue;
                var countOfSets = 5;
                var countOfPossibleSets = 4;
                while (countOfSets > countOfPossibleSets)
                {
                    countOfSets = validCombinationsCopy.Where(x => item[0].Equals(x[0])
                                                                && item[1].Equals(x[1])
                                                                && item[2].Equals(x[2]))
                                                       .Count();
                    if (countOfSets > countOfPossibleSets)
                    {
                        validCombinationsCopy.Remove(item);
                    }
                }
            }
            validCombinations = validCombinationsCopy;

            if (handNotCompleted)
                return new List<IEnumerable<TileKinds>>() { validCombinations };

            var possibleCombinations =
                new TileKinds(Enumerable.Range(0, validCombinations.Count))
                    .Permutations(countOfNeededCombinations);

            var combinationsResults = new List<IEnumerable<TileKinds>>();
            foreach (var combination in possibleCombinations)
            {
                var result = new List<TileKind>();
                foreach (var item in combination)
                {
                    result.AddRange(validCombinations[item.Value]);
                }
                result.Sort((x, y) => x.CompareTo(y));

                if (!indices.Equals(new TileKinds(result))) continue;

                var results = new List<TileKinds>();
                foreach (var item in combination)
                {
                    results.Add(validCombinations[item.Value]);
                }
                results.Sort((x, y) => x[0].CompareTo(y[0]));
                if (!combinationsResults.Contains_(results))
                {
                    combinationsResults.Add(results);
                }
            }
            return combinationsResults;
        }

        /// <summary>
        /// 各TileKindsが取りうる順列を列挙します。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<TileKinds> Permutations(this TileKinds source, int count)
        {
            var result = new List<TileKinds>();
            PermutationsInnner(source, count, new TileKinds(), result);
            return result;
        }

        private static void PermutationsInnner(TileKinds stock, int depth, TileKinds current, IList<TileKinds> result)
        {
            if (depth == 0)
            {
                result.Add(current);
                return;
            }
            for (var i = 0; i < stock.Count(); i++)
            {
                var copyOfStock = new TileKinds(stock);
                var copyOfCurrent = new TileKinds(current) { copyOfStock[i] };
                copyOfStock.RemoveAt(i);
                PermutationsInnner(copyOfStock, depth - 1, copyOfCurrent, result);
            }
        }

        private static bool Contains_<T>(this IEnumerable<T> source, T item) where T : IEnumerable<TileKinds>
        {
            return source.Any(x =>
            {
                var y = x.ToList();
                var z = item.ToList();
                if (y.Count != z.Count) return false;
                for (var i = 0; i < y.Count; i++)
                {
                    if (!z[i].Equals(y[i])) return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 各色の牌姿の直積を求めます。
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        private static IEnumerable<IEnumerable<IEnumerable<TileKinds>>> Product(IList<IEnumerable<IEnumerable<TileKinds>>> arrays)
        {
            var countOfArrays = arrays.Count;
            if (countOfArrays == 1)
            {
                return arrays;
            }
            var result = new List<IEnumerable<IEnumerable<TileKinds>>>();
            if (countOfArrays == 2)
            {
                foreach (var i in arrays[0])
                {
                    foreach (var j in arrays[1])
                    {
                        result.Add(new List<IEnumerable<TileKinds>> { i, j });
                    }
                }
            }
            if (countOfArrays == 3)
            {
                foreach (var i in arrays[0])
                {
                    foreach (var j in arrays[1])
                    {
                        foreach (var k in arrays[2])
                        {
                            result.Add(new List<IEnumerable<TileKinds>> { i, j, k });
                        }
                    }
                }
            }
            if (countOfArrays == 4)
            {
                foreach (var i in arrays[0])
                {
                    foreach (var j in arrays[1])
                    {
                        foreach (var k in arrays[2])
                        {
                            foreach (var l in arrays[3])
                            {
                                result.Add(new List<IEnumerable<TileKinds>> { i, j, k, l });
                            }
                        }
                    }
                }
            }
            if (countOfArrays == 5)
            {
                foreach (var i in arrays[0])
                {
                    foreach (var j in arrays[1])
                    {
                        foreach (var k in arrays[2])
                        {
                            foreach (var l in arrays[3])
                            {
                                foreach (var m in arrays[4])
                                {
                                    result.Add(new List<IEnumerable<TileKinds>> { i, j, k, l, m });
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}