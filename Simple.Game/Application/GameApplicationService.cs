using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Game.Domain;
using Simple.Game.Infrastructure;
using System;

namespace Simple.Game.Application
{
    public class GameApplicationService
    {
        public event EventHandler<JoinEventArgs> JoinEvent;

        private readonly IMessageServer server_;

        private readonly GameManager gameManager_;

        public GameApplicationService()
        {
            server_ = new MessageServer();
            server_.MessageReceivedEvent += OnMessageReceived;

            var rules = new GameOptionalRules();
            gameManager_ = new GameManager(rules);
        }

        public void Start()
        {
            gameManager_.Start();
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                case "Join":
                    var joinReq = JsonConvert.DeserializeObject<JoinReq>(message.Body);
                    JoinEvent(this, new JoinEventArgs(joinReq));
                    ResponseJoin();
                    Start();
                    break;

                case "Hand":
                    var handReq = JsonConvert.DeserializeObject<HandReq>(message.Body);
                    ResponseHand();
                    break;

                case "Tsumo":
                    var tsumoReq = JsonConvert.DeserializeObject<TsumoReq>(message.Body);
                    ResponseTsumo();
                    break;

                default:
                    break;
            }
        }

        private void ResponseJoin()
        {
            var joinRes = new JoinRes
            {
                Id = 0
            };
            var message = new Message
            {
                Header = "Join",
                Body = JsonConvert.SerializeObject(joinRes)
            };
            server_.SendMessage(JsonConvert.SerializeObject(message));
        }

        private void ResponseHand()
        {
            var handRes = new HandRes
            {
                Hand = gameManager_.Hand
            };
            var message = new Message
            {
                Header = "Hand",
                Body = JsonConvert.SerializeObject(handRes)
            };
            server_.SendMessage(JsonConvert.SerializeObject(message));
        }

        private void ResponseTsumo()
        {
            var tsumoRes = new TsumoRes
            {
                Tsumo = gameManager_.Tsumo()
            };
            var message = new Message
            {
                Header = "Tsumo",
                Body = JsonConvert.SerializeObject(tsumoRes)
            };
            server_.SendMessage(JsonConvert.SerializeObject(message));
        }
    }
}