﻿using Newtonsoft.Json;
using Simple.Common.Models;
using Simple.Player.Domain;
using System;

namespace Simple.Player.Application
{
    public class PlayerApplicationService
    {
        public event EventHandler<TsumoEventArgs> TsumoEvent;

        private readonly IMessageReceiver receiver_;
        private readonly IMessageSender sender_;

        public PlayerApplicationService()
        {
            receiver_.MessageReceivedEvent += Receiver__MessageReceivedEvent;
        }

        private void Receiver__MessageReceivedEvent(object sender, MessageReceivedEventArgs e)
        {
            var message = JsonConvert.DeserializeObject<Message>(e.Message);
            switch (message.Header)
            {
                case "Tsumo":
                    var body = JsonConvert.DeserializeObject<TsumoNotification>(message.Body);
                    TsumoEvent(this, new TsumoEventArgs(body));
                    break;

                default:
                    break;
            }
        }

        public void Dahai(int index, bool IsRiichi)
        {
            var body = new DahaiRequest(index, IsRiichi);
            var message = new Message("Dahai", JsonConvert.SerializeObject(body));
            sender_.Send(JsonConvert.SerializeObject(message));
        }
    }
}