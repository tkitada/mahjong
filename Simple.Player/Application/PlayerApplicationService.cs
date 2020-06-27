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

        private readonly IMessageClient client_;

        private readonly string name_;

        public PlayerApplicationService(string name)
        {
            client_ = new MessageClient();

            name_ = name;
            client_.MessageReceivedEvent += OnMessageReceived;

            Join();
        }

        private void Join()
        {
            var req = new JoinReq
            {
                Name = name_
            };
            var message = new Message
            {
                Header = "Join",
                Body = JsonConvert.SerializeObject(req)
            };
            client_.SendMessage(JsonConvert.SerializeObject(message));
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                case "Join":
                    var joinRes = JsonConvert.DeserializeObject<JoinRes>(message.Body);
                    JoinEvent(this, new JoinEventArgs(joinRes));
                    break;

                default:
                    break;
            }
        }
    }
}