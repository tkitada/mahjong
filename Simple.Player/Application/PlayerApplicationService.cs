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

        private readonly IMessageClient client_;

        private readonly string name_;

        public PlayerApplicationService(string name)
        {
            client_ = new MessageClient();

            name_ = name;
            client_.MessageReceivedEvent += OnMessageReceived;

            RequestJoin();
            RequestHand();
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
                    break;

                case "Hand":
                    var handRes = JsonConvert.DeserializeObject<HandRes>(message.Body);
                    HandEvent(this, new HandEventArgs
                    {
                        HandRes = handRes
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
            var HandReq = new HandReq { };
            var message = new Message
            {
                Header = "Hand",
                Body = JsonConvert.SerializeObject(HandReq)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }
    }
}