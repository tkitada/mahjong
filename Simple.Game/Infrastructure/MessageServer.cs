using Simple.Common;
using Simple.Game.Domain;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Simple.Game.Infrastructure
{
    internal class MessageServer : NamedPipeCommon, IMessageServer
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        private NamedPipeServerStream ps_;
        private NamedPipeClientStream pc_;

        public MessageServer()
        {
            WaitConnection();
        }

        public void SendMessage(string message)
        {
            pc_ = new NamedPipeClientStream(".", PlayerServerPipeName, PipeDirection.InOut);
            if (!pc_.IsConnected)
            {
                pc_.Connect();
            }
            if (pc_.IsConnected)
            {
                using (var sw = new StreamWriter(pc_))
                {
                    sw.Write(message);
                    sw.Flush();
                    pc_.WaitForPipeDrain();
                }
            }
        }

        private void WaitConnection()
        {

            _ = Task.Run(() =>
            {
                while (true)
                {
                    ps_ = new NamedPipeServerStream(GameServerPipeName, PipeDirection.InOut);
                    ps_.WaitForConnection();

                    using (var sr = new StreamReader(ps_))
                    {
                        var message = sr.ReadToEnd();
                        MessageReceivedEvent(this, new MessageReceivedEventArgs(message));
                    }
                }
            });
        }
    }
}