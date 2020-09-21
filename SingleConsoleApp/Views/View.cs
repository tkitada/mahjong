using SingleConsoleApp.Model.Domain;
using SingleConsoleApp.ViewModels;
using System.Timers;
using static System.Console;

namespace SingleConsoleApp.Views
{
    public class View
    {
        private static readonly Timer timer_ = new Timer(1000 / 30.0);
        private static readonly ViewModel vm_ = new ViewModel();

        private static void Main()
        {
            CursorVisible = false;
            timer_.Elapsed += (_, __) =>
            {
                timer_.Stop();
                Update();
                Draw();
                timer_.Start();
            };
            timer_.Start();

            while (true)
            {
                vm_.Input(ReadKey(true));
            };
        }

        private static void Update()
        {
            var preMode = vm_.Mode;
            vm_.Update();
            if (preMode != vm_.Mode && (preMode == ModeType.DisplayResult || vm_.Mode == ModeType.DisplayResult))
            {
                Clear();
            }
        }

        private static void Draw()
        {
            SetCursorPosition(0, 0);

            WriteLine($"ドラ表示牌:{ToCharacter(vm_.DoraIndicators[0].Value)}\n");
            foreach (var tileId in vm_.Hand.SortedTehai)
            {
                Write(ToCharacter(tileId.Value));
            }
            Write("　");
            if (vm_.Hand.TsumoTile != null)
            {
                Write(ToCharacter(vm_.Hand.TsumoTile.Value));
            }
            WriteLine("");

            if (vm_.Mode == ModeType.DisplayResult)
            {
                WriteLine("");
                if (vm_.Result is null)
                {
                    for (var i = 0; i < vm_.Discards.Count; i++)
                    {
                        Write(ToCharacter(vm_.Discards[i]));
                        if (i % 6 == 5)
                        {
                            WriteLine("");
                        }
                    }
                    WriteLine("");
                    WriteLine("流局");
                    return;
                }
                vm_.Result.Yaku.ForEach(
                    yakuItem => WriteLine($"{yakuItem.Japanese.PadRight(8, '　')}{yakuItem.HanClosed.ToString().PadLeft(5, '　')}翻"));
                WriteLine($"{vm_.Result.Han}翻 {vm_.Result.Fu}符");
                WriteLine($"{vm_.Result.Cost.Main * 2}点");
                vm_.Result.FuDetailSet.ForEach(
                    fuItem => WriteLine($"符: {fuItem.Fu}\tReason: {fuItem.Reason}"));
                return;
            }
            for (var i = 0; i < 13; i++)
            {
                Write(vm_.TileCursor.Position == i ? "↑" : "　");
            }
            Write("　");
            Write(vm_.TileCursor.Position == 13 ? "↑" : "　");
            WriteLine(vm_.Mode == ModeType.ConfirmAgari ? "\n和了? Yes: Enter / No: Space" : "\n");

            for (var i = 0; i < vm_.Discards.Count; i++)
            {
                Write(ToCharacter(vm_.Discards[i]));
                if (i % 6 == 5)
                {
                    WriteLine("");
                }
            }
        }

        private static string ToCharacter(int id)
        {
            var t = id / 4;
            return t switch
            {
                0 => "一",
                1 => "二",
                2 => "三",
                3 => "四",
                4 => "五",
                5 => "六",
                6 => "七",
                7 => "八",
                8 => "九",
                9 => "①",
                10 => "②",
                11 => "③",
                12 => "④",
                13 => "⑤",
                14 => "⑥",
                15 => "⑦",
                16 => "⑧",
                17 => "⑨",
                18 => "１",
                19 => "２",
                20 => "３",
                21 => "４",
                22 => "５",
                23 => "６",
                24 => "７",
                25 => "８",
                26 => "９",
                27 => "東",
                28 => "南",
                29 => "西",
                30 => "北",
                31 => "白",
                32 => "發",
                33 => "中",
                _ => "",
            };
        }
    }
}