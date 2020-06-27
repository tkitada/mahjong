using Simple.Player.Domain;

namespace Simple.Player.Infrastructure
{
    internal class MessageSender : IMessageSender
    {
        private readonly IMessageClient client_;

        public MessageSender(IMessageClient client)
        {
            client_ = client;
        }

        public void Send(string message)
        {
            client_.SendMessage(message);
        }
    }
}