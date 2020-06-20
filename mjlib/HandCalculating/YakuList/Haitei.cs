using mjlib.Tiles;
using System.Collections.Generic;

namespace mjlib.HandCalculating.YakuList
{
    internal class Haitei : Yaku
    {
        public override int YakuId => 5;
        public override int TenhouId => 5;
        public override string Name => "Haitei Raoyue";
        public override string Japanese => "海底摸月";
        public override string English => "Win By Last Draw";
        public override int HanOpen { get; set; } = 1;
        public override int HanClosed { get; set; } = 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            return true;
        }
    }
}