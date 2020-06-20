using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using mjlib;
using mjlib.HandCalculating;
using static mjlib.HandCalculating.ScoresCalcurator;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class ScoresCalculationTest
    {
        [TestMethod]
        public void CalculateScoresAndRonTest()
        {
            var config = new HandConfig(
                options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_NO_LIMIT));

            var result = CalculateScores(han: 1, fu: 30, config: config);
            AreEqual(1000, result.Main);

            result = CalculateScores(han: 1, fu: 110, config: config);
            AreEqual(3600, result.Main);

            result = CalculateScores(han: 2, fu: 30, config: config);
            AreEqual(2000, result.Main);

            result = CalculateScores(han: 3, fu: 30, config: config);
            AreEqual(3900, result.Main);

            result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(7700, result.Main);

            result = CalculateScores(han: 4, fu: 40, config: config);
            AreEqual(8000, result.Main);

            result = CalculateScores(han: 5, fu: 0, config: config);
            AreEqual(8000, result.Main);

            result = CalculateScores(han: 6, fu: 0, config: config);
            AreEqual(12000, result.Main);

            result = CalculateScores(han: 8, fu: 0, config: config);
            AreEqual(16000, result.Main);

            result = CalculateScores(han: 11, fu: 0, config: config);
            AreEqual(24000, result.Main);

            result = CalculateScores(han: 13, fu: 0, config: config);
            AreEqual(32000, result.Main);

            result = CalculateScores(han: 26, fu: 0, config: config);
            AreEqual(64000, result.Main);

            result = CalculateScores(han: 39, fu: 0, config: config);
            AreEqual(96000, result.Main);

            result = CalculateScores(han: 52, fu: 0, config: config);
            AreEqual(128000, result.Main);

            result = CalculateScores(han: 65, fu: 0, config: config);
            AreEqual(160000, result.Main);

            result = CalculateScores(han: 78, fu: 0, config: config);
            AreEqual(192000, result.Main);
        }

        [TestMethod]
        public void CalculateScoresAndRonByDealerTest()
        {
            var config = new HandConfig(playerWind: Constants.EAST,
                options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_NO_LIMIT));

            var result = CalculateScores(han: 1, fu: 30, config: config);
            AreEqual(1500, result.Main);

            result = CalculateScores(han: 2, fu: 30, config: config);
            AreEqual(2900, result.Main);

            result = CalculateScores(han: 3, fu: 30, config: config);
            AreEqual(5800, result.Main);

            result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(11600, result.Main);

            result = CalculateScores(han: 5, fu: 0, config: config);
            AreEqual(12000, result.Main);

            result = CalculateScores(han: 6, fu: 0, config: config);
            AreEqual(18000, result.Main);

            result = CalculateScores(han: 8, fu: 0, config: config);
            AreEqual(24000, result.Main);

            result = CalculateScores(han: 11, fu: 0, config: config);
            AreEqual(36000, result.Main);

            result = CalculateScores(han: 13, fu: 0, config: config);
            AreEqual(48000, result.Main);

            result = CalculateScores(han: 26, fu: 0, config: config);
            AreEqual(96000, result.Main);

            result = CalculateScores(han: 39, fu: 0, config: config);
            AreEqual(144000, result.Main);

            result = CalculateScores(han: 52, fu: 0, config: config);
            AreEqual(192000, result.Main);

            result = CalculateScores(han: 65, fu: 0, config: config);
            AreEqual(240000, result.Main);

            result = CalculateScores(han: 78, fu: 0, config: config);
            AreEqual(288000, result.Main);
        }

        [TestMethod]
        public void CalculateScoresAndTsumo()
        {
            var config = new HandConfig(isTsumo: true,
                options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_NO_LIMIT));

            var result = CalculateScores(han: 1, fu: 30, config: config);
            AreEqual(500, result.Main);
            AreEqual(300, result.Additional);

            result = CalculateScores(han: 3, fu: 30, config: config);
            AreEqual(2000, result.Main);
            AreEqual(1000, result.Additional);

            result = CalculateScores(han: 3, fu: 60, config: config);
            AreEqual(3900, result.Main);
            AreEqual(2000, result.Additional);

            result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(3900, result.Main);
            AreEqual(2000, result.Additional);

            result = CalculateScores(han: 5, fu: 0, config: config);
            AreEqual(4000, result.Main);
            AreEqual(2000, result.Additional);

            result = CalculateScores(han: 6, fu: 0, config: config);
            AreEqual(6000, result.Main);
            AreEqual(3000, result.Additional);

            result = CalculateScores(han: 8, fu: 0, config: config);
            AreEqual(8000, result.Main);
            AreEqual(4000, result.Additional);

            result = CalculateScores(han: 11, fu: 0, config: config);
            AreEqual(12000, result.Main);
            AreEqual(6000, result.Additional);

            result = CalculateScores(han: 13, fu: 0, config: config);
            AreEqual(16000, result.Main);
            AreEqual(8000, result.Additional);

            result = CalculateScores(han: 26, fu: 0, config: config);
            AreEqual(32000, result.Main);
            AreEqual(16000, result.Additional);

            result = CalculateScores(han: 39, fu: 0, config: config);
            AreEqual(48000, result.Main);
            AreEqual(24000, result.Additional);

            result = CalculateScores(han: 52, fu: 0, config: config);
            AreEqual(64000, result.Main);
            AreEqual(32000, result.Additional);

            result = CalculateScores(han: 65, fu: 0, config: config);
            AreEqual(80000, result.Main);
            AreEqual(40000, result.Additional);

            result = CalculateScores(han: 78, fu: 0, config: config);
            AreEqual(96000, result.Main);
            AreEqual(48000, result.Additional);
        }

        [TestMethod]
        public void CalcylateScoresAndTsumoByDealerTest()
        {
            var config = new HandConfig(playerWind: Constants.EAST, isTsumo: true,
                options: new OptionalRules(kazoeLimit: HandConstans.KAZOE_NO_LIMIT));

            var result = CalculateScores(han: 1, fu: 30, config: config);
            AreEqual(500, result.Main);
            AreEqual(500, result.Additional);

            result = CalculateScores(han: 3, fu: 30, config: config);
            AreEqual(2000, result.Main);
            AreEqual(2000, result.Additional);

            result = CalculateScores(han: 3, fu: 60, config: config);
            AreEqual(3900, result.Main);
            AreEqual(3900, result.Additional);

            result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(3900, result.Main);
            AreEqual(3900, result.Additional);

            result = CalculateScores(han: 5, fu: 0, config: config);
            AreEqual(4000, result.Main);
            AreEqual(4000, result.Additional);

            result = CalculateScores(han: 6, fu: 0, config: config);
            AreEqual(6000, result.Main);
            AreEqual(6000, result.Additional);

            result = CalculateScores(han: 8, fu: 0, config: config);
            AreEqual(8000, result.Main);
            AreEqual(8000, result.Additional);

            result = CalculateScores(han: 11, fu: 0, config: config);
            AreEqual(12000, result.Main);
            AreEqual(12000, result.Additional);

            result = CalculateScores(han: 13, fu: 0, config: config);
            AreEqual(16000, result.Main);
            AreEqual(16000, result.Additional);

            result = CalculateScores(han: 26, fu: 0, config: config);
            AreEqual(32000, result.Main);
            AreEqual(32000, result.Additional);

            result = CalculateScores(han: 39, fu: 0, config: config);
            AreEqual(48000, result.Main);
            AreEqual(48000, result.Additional);

            result = CalculateScores(han: 52, fu: 0, config: config);
            AreEqual(64000, result.Main);
            AreEqual(64000, result.Additional);

            result = CalculateScores(han: 65, fu: 0, config: config);
            AreEqual(80000, result.Main);
            AreEqual(80000, result.Additional);

            result = CalculateScores(han: 78, fu: 0, config: config);
            AreEqual(96000, result.Main);
            AreEqual(96000, result.Additional);
        }

        [TestMethod]
        public void KiriageManganTest()
        {
            var config = new HandConfig(
                options: new OptionalRules(kiriage: true));

            var result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(8000, result.Main);

            result = CalculateScores(han: 3, fu: 60, config: config);
            AreEqual(8000, result.Main);

            config = new HandConfig(playerWind: Constants.EAST,
                options: new OptionalRules(kiriage: true));

            result = CalculateScores(han: 4, fu: 30, config: config);
            AreEqual(12000, result.Main);

            result = CalculateScores(han: 3, fu: 60, config: config);
            AreEqual(12000, result.Main);
        }
    }
}