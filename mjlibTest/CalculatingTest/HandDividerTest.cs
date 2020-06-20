using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.HandCalculating.HandDivider;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class HandDividerTest
    {
        [TestMethod]
        public void SimpleHandDividingTest()
        {
            var tiles34 = Tiles34.Parse(man: "234567", sou: "23455", honors: "777");
            var result = DivideHand(tiles34);
            AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "234m","567m","234s","55s","777z"
            }, ToString(result[0]));
        }

        [TestMethod]
        public void SecondSimpleHandDividing()
        {
            var tiles34 = Tiles34.Parse(man: "123", pin: "123", sou: "123", honors: "11222");
            var result = DivideHand(tiles34);
            AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "123m","123p","123s","11z","222z"
            }, ToString(result[0]));
        }

        [TestMethod]
        public void HandWithPairsDividing()
        {
            var tiles34 = Tiles34.Parse(man: "23444", pin: "344556", sou: "333");
            var result = DivideHand(tiles34);
            AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "234m","44m","345p","456p","333s"
            }, ToString(result[0]));
        }

        [TestMethod]
        public void OneSuitHandDividingTest()
        {
            var tiles34 = Tiles34.Parse(man: "11122233388899");
            var result = DivideHand(tiles34);
            AreEqual(2, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "111m","222m","333m","888m","99m"
            }, ToString(result[0]));
            CollectionAssert.AreEqual(new List<string>
            {
                "123m","123m","123m","888m","99m"
            }, ToString(result[1]));
        }

        [TestMethod]
        public void SecondOneSuitHandDividing()
        {
            var tiles34 = Tiles34.Parse(sou: "111123666789", honors: "11");
            var result = DivideHand(tiles34);
            AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "111s","123s","666s","789s","11z"
            }, ToString(result[0]));
        }

        [TestMethod]
        public void ThirdOneSuitHandDividing()
        {
            var tiles34 = Tiles34.Parse(pin: "234777888999", honors: "22");
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.CHI, pin:"789"),
                MakeMeld(MeldType.CHI, pin:"234")
            };
            var result = DivideHand(tiles34, melds);
            AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "234p","789p","789p","789p","22z"
            }, ToString(result[0]));
        }

        [TestMethod]
        public void ChiitoitsuLikeHandDividing()
        {
            var tiles34 = Tiles34.Parse(man: "112233", pin: "99", sou: "445566");
            var result = DivideHand(tiles34);
            AreEqual(2, result.Count);
            CollectionAssert.AreEqual(new List<string>
            {
                "11m", "22m", "33m", "99p", "44s", "55s", "66s"
            }, ToString(result[0]));
            CollectionAssert.AreEqual(new List<string>
            {
                "123m", "123m", "99p", "456s", "456s"
            }, ToString(result[1]));
        }

        private List<string> ToString(IEnumerable<TileKinds> hand)
        {
            var results = new List<string>();
            foreach (var setItem in hand)
            {
                results.Add(
                    new TileIds(setItem.Select(x => x.Value * 4)).ToOneLineString());
            }
            return results;
        }
    }
}