using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Game.Domain;

namespace Simple.Game.Application
{
    public class GameApplicationService
    {
        private readonly IMessageReceiver receiver_;
        private readonly IMessageSebder sender_;

        private readonly GameManager gameManager_;

        public GameApplicationService()
        {
            receiver_.MessageReceivedEvent += OnReceivedMessage;

            var rules = new GameOptionalRules();
            gameManager_ = new GameManager(rules);

            gameManager_.HandNotificationEvent += NotifyHand;
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
                case "Dahai":
                    var body = JsonConvert.DeserializeObject<DahaiRequest>(message.Body);
                    break;

                default:
                    break;
            }
        }

        private void NotifyHand(object sender, HandNotificationEventArgs e)
        {
            var message = new Message("Hand", JsonConvert.SerializeObject(e.Hand));
            sender_.Send(JsonConvert.SerializeObject(message));
        }
    }
}