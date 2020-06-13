using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList.Yakuman
{
    internal class Suuankou : Yaku
    {
        public override int YakuId => 38;

        public override int TenhouId => 41;

        public override string Name => "Suuankou";

        public override string Japanese => "四暗刻";

        public override string English => "Four Concealed Triplets";

        public override int HanOpen => 0;

        public override int HanClosed => 13;

        public override bool IsYakuman => true;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var winTile = ((TileId)args[0]).ToTileKind();
            var isTsumo = (bool)args[1];
            var closedHand = new List<TileKinds>();
            foreach (var item in hand)
            {
                if (item.IsPon && item.Contains(winTile) && !isTsumo) continue;
                closedHand.Add(item);
            }
            return closedHand.Count(x => x.IsPon) == 4;
        }
    }
}