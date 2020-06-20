using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Tsumo : Yaku
    {
        public override int YakuId => 0;
        public override int TenhouId => 0;
        public override string Name => "Menzen Tsumo";
        public override string Japanese => "門前清自摸和";
        public override string English => "Self Draw";
        public override int HanOpen { get; set; } = 0;
        public override int HanClosed { get; set; } = 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}