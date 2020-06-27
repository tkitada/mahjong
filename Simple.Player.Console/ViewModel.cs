using Simple.Player.Application;
using System;

namespace Simple.Player.Console
{
    internal class ViewModel
    {
        private readonly PlayerApplicationService appService_;

        public ViewModel()
        {
            appService_ = new PlayerApplicationService();

        }

    }
}