using mjlib.HandCalculating;
using mjlib.Tiles;
using SingleConsoleApp.Model.ApplicationService;
using SingleConsoleApp.Model.Domain;
using System;
using System.Collections.Generic;

namespace SingleConsoleApp.ViewModels
{
    internal class ViewModel
    {
        public TileIds DoraIndicators { get; private set; }
        public Hand Hand { get; private set; }
        public List<int> Discards { get; private set; }
        public HandResponse Result { get; private set; }
        public bool ResultDraw { get; private set; }
        public TileCursor TileCursor { get; private set; }
        public ModeType Mode { get; private set; }

        private readonly SingleConsoleApplicationService appService_ = new SingleConsoleApplicationService();

        public void Update()
        {
            var items = appService_.Update();
            DoraIndicators = items.DoraIndicators;
            Hand = items.Hand;
            Discards = items.Discards;
            Result = items.Result;
            TileCursor = items.TileCursor;
            Mode = items.Mode;
        }

        internal void Input(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    appService_.MoveCursor(Direction.Left);
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    appService_.MoveCursor(Direction.Right);
                    break;

                case ConsoleKey.Enter:
                    switch (Mode)
                    {
                        case ModeType.Normal:
                            appService_.Dahai(false);
                            break;

                        case ModeType.ConfirmAgari:
                            appService_.Agari();
                            break;

                        case ModeType.DisplayResult:
                            appService_.Reset();
                            break;

                        default:
                            break;
                    }
                    break;

                case ConsoleKey.Spacebar:
                    switch (Mode)
                    {
                        case ModeType.Normal:
                            appService_.Dahai(true);
                            break;

                        case ModeType.ConfirmAgari:
                            appService_.CancelAgari();
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}