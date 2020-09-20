using mjlib.HandCalculating;
using mjlib.Tiles;
using System.Collections.Generic;

namespace SingleConsoleApp.Model.Domain
{
    internal class GameManager
    {
        private readonly OptionalRules rules_ = new OptionalRules();
        private readonly Wall wall_;
        public TileId DoraIndicate => wall_.DoraIndicate;
        public Hand Hand { get; }
        public List<int> Discards { get; private set; } = new List<int>();

        public GameManager()
        {
            wall_ = new Wall();
            Hand = new Hand(wall_.Haipai());
            Tsumo();
        }

        public void Tsumo()
        {
            Hand.Tsumo(wall_.Tsumo());
        }

        public void Dahai(int index)
        {
            if (index != 13)
            {
                index = Hand.Tehai.IndexOf(Hand.SortedTehai[index]);
            }
            Discards.Add(Hand.Dahai(index).Value);

            Tsumo();
        }

            //ツモ判定
            //ツモ 和了判定を返すようにする
            //和了可能時選択肢を出すようにする
            //和了不可能時、或いは和了を選択しなかったとき打牌
            //打牌　立直時は自動でツモ切り？
    }
}