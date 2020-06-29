using Simple.Common;
using Simple.Player.Domain;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Simple.Player.Infrastructure
{
    internal class MessageClient : NamedPipeCommon, IMessageClient
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        private NamedPipeServerStream ps_;
        private NamedPipeClientStream pc_;

        public MessageClient()
        {
            WaitConnection();
        }

        public void SendMessage(string message)
        {
            pc_ = new NamedPipeClientStream(".", GameServerPipeName, PipeDirection.InOut);
            pc_.Connect();
            if (pc_.IsConnected)
            {
                using (var sw = new StreamWriter(pc_))
                {
                    sw.Write(message);
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
                    ps_ = new NamedPipeServerStream(PlayerServerPipeName, PipeDirection.InOut);
                    ps_.WaitForConnection();

                    using (var sr = new StreamReader(ps_))
                    {
                        var message = sr.ReadToEnd();
                        MessageReceivedEvent?.Invoke(this, new MessageReceivedEventArgs(message));
                    }
                }
            });
        }
    }
}