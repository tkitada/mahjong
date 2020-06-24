using Simple.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Game.Application
{
    class DahaiEventArgs : EventArgs
    {
        public DahaiRequest Dahai { get; }

        public DahaiEventArgs(DahaiRequest dahai)
        {
            Dahai = dahai;
        }
    }
}
