using Simple.Game.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Game.Console
{
    class ViewModel
    {
        private GameApplicationService appService_;
        public ViewModel()
        {
            appService_ = new GameApplicationService();
        }
    }
}
