using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Game.Domain
{
    class ClientManager
    {
        private string name_;
        public void Join(string name)
        {
            name_ = name;
        }
    }
}
