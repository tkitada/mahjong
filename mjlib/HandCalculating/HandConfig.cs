using static mjlib.Constants;

namespace mjlib.HandCalculating
{
    public class HandConfig
    {
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
        public bool IsChiihou { get; }
        public bool IsRenhou { get; }
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

    public class OptionalRules
    {
        public bool HasOpenTanyao { get; }
        public bool HasAkaDora { get; set; } = false;
        public bool HasDoubleYakuman { get; }
        public Kazoe KazoeLimit { get; }
        public bool Kiriage { get; }
        public bool FuForOpenPinfu { get; }
        public bool FuForPinfuTsumo { get; }
        public bool RenhouAsYakuman { get; }
        public bool HasDaisharin { get; }

        public OptionalRules(bool hasOpenTanyao = false,
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