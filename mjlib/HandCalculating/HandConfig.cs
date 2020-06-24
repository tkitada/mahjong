using static mjlib.Constants;

namespace mjlib.HandCalculating
{
    /// <summary>
    /// 点数に関わる場の状況
    /// </summary>
    public class HandConfig
    {
        public OptionalRules Options { get; }

        //ツモ
        public bool IsTsumo { get; }
        //リーチ
        public bool IsRiichi { get; }
        //一発
        public bool IsIppatsu { get; }
        //嶺上開花
        public bool IsRinshan { get; }
        //槍槓
        public bool IsChankan { get; }
        //海底撈月
        public bool IsHaitei { get; }
        //河底撈魚
        public bool IsHoutei { get; }
        //ダブル立直
        public bool IsDaburuRiichi { get; }
        //流し満貫
        public bool IsNagashiMangan { get; }
        //天和
        public bool IsTenhou { get; }
        //地和
        public bool IsChiihou { get; }
        //人和
        public bool IsRenhou { get; }
        //親
        public bool IsDealer { get; }
        //自風
        public int PlayerWind { get; }
        //場風
        public int RoundWind { get; }

        public HandConfig(
            OptionalRules options = null,
            bool isTsumo = false,
            bool isRiichi = false,
            bool isIppatsu = false,
            bool isRinshan = false,
            bool isChankan = false,
            bool isHaitei = false,
            bool isHoutei = false,
            bool isDaburuRiichi = false,
            bool isNagashiMangan = false,
            bool isTenhou = false,
            bool isChiihou = false,
            bool isRenhou = false,
            int playerWind = -1,
            int roundWind = -1)
        {
            Options = options ?? new OptionalRules();
            IsTsumo = isTsumo;
            IsRiichi = isRiichi;
            IsIppatsu = isIppatsu;
            IsRinshan = isRinshan;
            IsChankan = isChankan;
            IsHaitei = isHaitei;
            IsHoutei = isHoutei;
            IsDaburuRiichi = isDaburuRiichi;
            IsNagashiMangan = isNagashiMangan;
            IsTenhou = isTenhou;
            IsChiihou = isChiihou;
            IsRenhou = isRenhou;
            PlayerWind = playerWind;
            RoundWind = roundWind;

            IsDealer = playerWind == EAST;
        }
    }

    public enum Kazoe
    {
        Limited = 0,
        Sanbaiman = 1,
        Nolimit = 2
    }

    /// <summary>
    /// 点数に関わるルール
    /// </summary>
    public class OptionalRules
    {
        //喰いタン
        public bool HasOpenTanyao { get; }
        //赤ドラ(1枚ずつ)
        public bool HasAkaDora { get; }
        //ダブル役満
        public bool HasDoubleYakuman { get; }
        //数え役満
        public Kazoe KazoeLimit { get; }
        //切り上げ満貫
        public bool Kiriage { get; }
        //食い平和2符追加
        public bool FuForOpenPinfu { get; }
        //ピンヅモ
        public bool FuForPinfuTsumo { get; }
        //人和役満
        public bool RenhouAsYakuman { get; }
        //大車輪
        public bool HasDaisharin { get; }

        public OptionalRules(
            bool hasOpenTanyao = false,
            bool hasAkaDora = false,
            bool hasDoubleYakuman = true,
            Kazoe kazoeLimit = Kazoe.Limited,
            bool kiriage = false,
            bool fuForOpenPinfu = true,
            bool fuForPinfuTsumo = false,
            bool renhouAsYakuman = false,
            bool hasDaisharin = false)
        {
            HasOpenTanyao = hasOpenTanyao;
            HasAkaDora = hasAkaDora;
            HasDoubleYakuman = hasDoubleYakuman;
            KazoeLimit = kazoeLimit;
            Kiriage = kiriage;
            FuForOpenPinfu = fuForOpenPinfu;
            FuForPinfuTsumo = fuForPinfuTsumo;
            RenhouAsYakuman = renhouAsYakuman;
            HasDaisharin = hasDaisharin;
        }
    }
}