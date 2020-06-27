using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Game.Domain
{
    interface IMessageServer
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;
        void SendMessage(string message);

    }
}
