using mjlib.Tiles;
using SingleConsoleApp.Model.ApplicationService;
using SingleConsoleApp.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleConsoleApp.ViewModels
{
    internal class ViewModel
    {
        public TileId DoraIndicate { get; private set; }
        public Hand Hand { get; private set; }
        public List<int> Discards { get; private set; }
        public TileCursor TileCursor { get; private set; }

        private readonly SingleConsoleApplicationService appService_ = new SingleConsoleApplicationService();

        public void Update()
        {
            (DoraIndicate, Hand, Discards, TileCursor) = appService_.Update();
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
                    appService_.Select();
                    break;

                default:
                    break;
            }
        }
    }
}