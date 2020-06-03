using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib.Tiles;

namespace mjlibTest
{
    /// <summary>
    /// TileTest の概要の説明
    /// </summary>
    [TestClass]
    public class TileTest
    {
        public TileTest()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Tiles136ToOneLineStringTest()
        {
            var tiles = new Tiles136(new List<int>
            {
                0, 1, 34, 35, 36, 37, 70, 71, 72, 73, 106, 107, 108, 109, 133, 134
            });
            var expected = "1199m1199p1199s1177z";
            var actual = tiles.ToOneLineString();
            Assert.AreEqual(expected, actual);
        }
    }
}
