using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Game.Domain;
using Simple.Game.Infrastructure;

namespace Simple.Game.Application
{
    public class GameApplicationService
    {
        private readonly IMessageServer server_;
        private readonly IMessageSender sender_;
        private readonly IMessageReceiver receiver_;

        private readonly GameManager gameManager_;

        public GameApplicationService()
        {
            server_ = new MessageServer();
            sender_ = new MessageSender(server_);
            receiver_ = new MessageReceiver(server_);
            receiver_.MessageReceivedEvent += OnReceivedMessage;

            var rules = new GameOptionalRules();
            gameManager_ = new GameManager(rules);

        }

        public void Start()
        {
            gameManager_.Start();
        }

        private void OnReceivedMessage(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                default:
                    break;
            }
        }
    }
}