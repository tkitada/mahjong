using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Player.Domain;
using Simple.Player.Infrastructure;
using System;

namespace Simple.Player.Application
{
    public class PlayerApplicationService
    {
        private readonly IMessageClient client_;
        private readonly IMessageSender sender_;
        private readonly IMessageReceiver receiver_;

        public PlayerApplicationService()
        {
            client_ = new MessageClient();
            sender_ = new MessageSender(client_);
            receiver_ = new MessageReceiver(client_);
            receiver_.MessageReceivedEvent += Receiver__MessageReceivedEvent;
        }

        private void Receiver__MessageReceivedEvent(object sender, MessageReceivedEventArgs e)
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