using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Player.Domain;
using Simple.Player.Infrastructure;
using System;

namespace Simple.Player.Application
{
    public class PlayerApplicationService
    {
        public event EventHandler<JoinEventArgs> JoinEvent;

        public event EventHandler<HandEventArgs> HandEvent;

        public event EventHandler<TsumoEventArgs> TsumoEvent;

        private readonly IMessageClient client_;

        private readonly string name_;

        public PlayerApplicationService(string name)
        {
            client_ = new MessageClient();

            name_ = name;
            client_.MessageReceivedEvent += OnMessageReceived;

            RequestJoin();
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                case "Join":
                    var joinRes = JsonConvert.DeserializeObject<JoinRes>(message.Body);
                    JoinEvent(this, new JoinEventArgs
                    {
                        JoinRes = joinRes
                    });
                    RequestHand();
                    break;

                case "Hand":
                    var handRes = JsonConvert.DeserializeObject<HandRes>(message.Body);
                    HandEvent(this, new HandEventArgs
                    {
                        HandRes = handRes
                    });
                    RequestTsumo();
                    break;

                case "Tsumo":
                    var tsumoRes = JsonConvert.DeserializeObject<TsumoRes>(message.Body);
                    TsumoEvent(this, new TsumoEventArgs
                    {
                        TsumoRes = tsumoRes
                    });
                    break;

                default:
                    break;
            }
        }

        private void RequestJoin()
        {
            var joinReq = new JoinReq
            {
                Name = name_
            };
            var message = new Message
            {
                Header = "Join",
                Body = JsonConvert.SerializeObject(joinReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        private void RequestHand()
        {
            var handReq = new HandReq { };
            var message = new Message
            {
                Header = "Hand",
                Body = JsonConvert.SerializeObject(handReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        private void RequestTsumo()
        {
            var tsumoReq = new TsumoReq { };
            var message = new Message
            {
                Header = "Tsumo",
                Body = JsonConvert.SerializeObject(tsumoReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }
    }
}