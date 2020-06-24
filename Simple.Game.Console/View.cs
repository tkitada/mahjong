using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Game.Console
{
    class View
    {
        private static ViewModel vm_;
        static void Main(string[] args)
        {
            vm_ = new ViewModel();
        }
    }
}
