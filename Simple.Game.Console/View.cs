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
        static void Main()
        {
            vm_ = new ViewModel();
            while (true)
            {
                var input = System.Console.ReadLine();
                if (input == "start")
                {
                    vm_.GameStart();
                }
            }
        }
    }
}
