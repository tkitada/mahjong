using mjlib.HandCalculating;

namespace Simple.Game.Domain
{
    /// <summary>
    /// 対局ルール
    /// </summary>
    internal class GameOptionalRules : OptionalRules
    {
        //半荘か東風か
        public bool IsHanchan { get; set; }

        //持ち点
        public int PrimaryPoint { get; set; }

        //返し点
        public int ReturnPoint { get; set; }

        //テンパイ連チャンかアガリ連チャンか
        public bool HasTenpaiRenchan { get; set; }

        //ダブロン
        public bool HasDoubleRon { get; set; }

        //四家立直で流局か
        public bool HasQuadrupleRiichi { get; set; }

        //延長によるサドンデス
        public bool HasSuddenDeath { get; set; }

        public GameOptionalRules(
            bool hasOpenTanyao = false,
            bool hasAkaDora = false,
            bool hasDoubleYakuman = true,
            Kazoe kazoeLimit = Kazoe.Limited,
            bool kiriage = false,
            bool fuForOpenPinfu = true,
            bool fuForPinfuTsumo = false,
            bool renhouAsYakuman = false,
            bool hasDaisharin = false,
            bool isHanchan = true,
            int primaryPoint = 25000,
            int returnPoint = 30000,
            bool hasTenpaiRenchan = true,
            bool hasDoubleRon = true,
            bool hasQuadrupleRiichi = true,
            bool hasSuddenDeath = true) : base(
                hasOpenTanyao,
                hasAkaDora,
                hasDoubleYakuman,
                kazoeLimit,
                kiriage,
                fuForOpenPinfu,
                fuForPinfuTsumo,
                renhouAsYakuman,
                hasDaisharin)
        {
            IsHanchan = isHanchan;
            PrimaryPoint = primaryPoint;
            ReturnPoint = returnPoint;
            HasTenpaiRenchan = hasTenpaiRenchan;
            HasDoubleRon = hasDoubleRon;
            HasQuadrupleRiichi = hasQuadrupleRiichi;
            HasSuddenDeath = hasSuddenDeath;
        }
    }
}