﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Player.Domain
{
    interface IMessageSender
    {
        void Send(string message);
    }
}
