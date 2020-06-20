using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;

namespace mjlib.HandCalculating.YakuList
{
    internal class Sanankou : Yaku
    {
        public override int YakuId => 27;

        public override int TenhouId => 29;

        public override string Name => "San Ankou";

        public override string Japanese => "三暗刻";

        public override string English => "Tripple Concealed Triplets";

        public override int HanOpen { get; set; } = 2;

        public override int HanClosed { get; set; } = 2;

        public override bool IsYakuman => false;

        public override bool IsConditionMet(IList<TileKinds> hand, object[] args = null)
        {
            var winTile = ((TileId)args[0]).ToTileKind();
            var melds = (List<Meld>)args[1];
            var isTsumo = (bool)args[2];

            var openSets = melds.Where(x => x.Opened)
                                .Select(x => x.TileKinds);
            var chiSets = hand.Where(x => x.IsChi
                && x.Contains(winTile) && !openSets.Contains(x));
            var ponSets = hand.Where(x => x.IsPon);
            var closedPonSets = new List<TileKinds>();
            foreach (var item in ponSets)
            {
                if (openSets.Contains(item)) continue;
                if (item.Contains(winTile) && !isTsumo && chiSets.Count() == 0)
                    continue;
                closedPonSets.Add(item);
            }
            return closedPonSets.Count == 3;
        }
    }
}