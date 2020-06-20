using static mjlib.Constants;

namespace mjlib.HandCalculating
{
    public class HandConfig : HandConstans
    {
        public Yaku Yaku { get; }
        public OptionalRules Options { get; }

        public bool IsTsumo { get; }
        public bool IsRiichi { get; }
        public bool IsIppatsu { get; }
        public bool IsRinshan { get; }
        public bool IsChankan { get; }
        public bool IsHaitei { get; }
        public bool IsHoutei { get; }
        public bool IsDaburuRiichi { get; }
        public bool IsNagashiMangan { get; }
        public bool IsTenhou { get; }
        public bool IsRenhou { get; }
        public bool IsChiihou { get; }
        public bool IsDealer { get; }
        public int PlayerWind { get; }
        public int RoundWind { get; }

        public HandConfig(Yaku yaku = null,
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
            bool isRenhou = false,
            bool isChiihou = false,
            int playerWind = -1,
            int roundWind = -1)
        {
            Yaku = yaku;
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
            IsRenhou = isRenhou;
            IsChiihou = isChiihou;
            PlayerWind = playerWind;
            RoundWind = roundWind;

            IsDealer = playerWind == EAST;
        }
    }

    public class HandConstans
    {
        //26翻以上を数えダブル役満としない
        public static int KAZOE_LIMITED => 0;

        //13翻以上は三倍満
        public static int KAZOE_SANBAIMAN => 1;

        //26翻以上はダブル役満、39翻以上はトリプル役満
        public static int KAZOE_NO_LIMIT => 2;
    }

    public class OptionalRules
    {
        public bool HasOpenTanyao { get; }
        public bool HasAkaDora { get; set; } = false;
        public bool HasDoubleYakuman { get; }
        public int KazoeLimit { get; }
        public bool Kiriage { get; }
        public bool FuForOpenPinfu { get; }
        public bool FuForPinfuTsumo { get; }
        public bool RenhouAsYakuman { get; }
        public bool HasDaisharin { get; }
        public bool HasDaisharinOtherSuits { get; }

        public OptionalRules(bool hasOpenTanyao = false,
            bool hasAkaDora = false,
            bool hasDoubleYakuman = true,
            int kazoeLimit = 0,
            bool kiriage = false,
            bool fuForOpenPinfu = true,
            bool fuForPinfuTsumo = false,
            bool renhouAsYakuman = false,
            bool hasDaisharin = false,
            bool hasDaisharinOtherSuits = false)
        {
            HasOpenTanyao = hasOpenTanyao;
            HasAkaDora = hasAkaDora;
            HasDoubleYakuman = hasDoubleYakuman;
            KazoeLimit = kazoeLimit;
            Kiriage = kiriage;
            FuForOpenPinfu = fuForOpenPinfu;
            FuForPinfuTsumo = fuForPinfuTsumo;
            RenhouAsYakuman = renhouAsYakuman;
            HasDaisharin = hasDaisharin || hasDaisharinOtherSuits;
            HasDaisharinOtherSuits = hasDaisharinOtherSuits;
        }
    }
}