using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common
{
   public abstract class NamedPipeCommon
    {
        public string GameServerPipeName => "mahjong.Game.Pipe";
        public string PlayerServerPipeName =>"mahjong.Player.Pipe";
    }
}
