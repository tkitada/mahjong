using Simple.Game.Domain;

namespace Simple.Game.Infrastructure
{
    internal class MessageSender : IMessageSender
    {
        private readonly IMessageServer server_;

        public MessageSender(IMessageServer server)
        {
            server_ = server;
        }

        public void Send(string message)
        {
            server_.SendMessage(message);
        }
    }
}