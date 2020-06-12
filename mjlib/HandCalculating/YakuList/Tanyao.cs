using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Tanyao : Yaku
    {
        public override int YakuId => 11;
        public override int TenhouId => 8;
        public override string Name => "Tanyao";
        public override string Japanese => "断么九";
        public override string English => "All Simples";
        public override int HanOpen => 1;
        public override int HanClosed => 1;
        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var indices = hand.Aggregate((x, y) => x.AddRange(y));
            return indices.All(x => x.IsChuchan);
        }
    }
}